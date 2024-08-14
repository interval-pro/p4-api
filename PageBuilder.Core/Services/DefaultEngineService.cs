using Newtonsoft.Json;
using PageBuilder.Core.Contracts;
using PageBuilder.Core.Models;
using PageBuilder.Core.Models.ComponentModels;
using PageBuilder.Core.Utilities;


namespace PageBuilder.Core.Services
{
    public class DefaultEngineService : IEngineService
    {
        private readonly IOpenAiService openAiService;
        private readonly IRetryPolicyService retryPolicy;

        public DefaultEngineService(
            IOpenAiService openAiService,
            IRetryPolicyService retryPolicy)
        {
            this.openAiService = openAiService;
            this.retryPolicy = retryPolicy;
        }

        public async Task<object> GenerateImageAsync(string input)
        {
            string imageUrl = string.Empty;

            try
            {
                imageUrl = await retryPolicy.ExecuteImageWithRetryAsync(() => openAiService.CreateImageFromTextAsync(input));

                if (string.IsNullOrEmpty(imageUrl))
                {
                    return "Failed to generate image";
                }
            }
            catch (Exception x)
            {
                return x.Message;
            }

            return new { url = imageUrl };
        }

        public async Task<LayoutModel?> GenerateLayoutAsync(CreateLayoutModel request)
        {
            var input = request.Inputs;

            string layout = string.Empty;
            layout = await retryPolicy.ExecuteLayoutWithRetryAsync(() => openAiService.CreateLayoutAsync(input));

            if (string.IsNullOrEmpty(layout))
            {
                return null;
            }

            LayoutModel? layoutModel = SharedFunctions.ParseLayout(layout, input);

            return layoutModel;
        }

        public async Task<SectionContent?> GenerateSectionAsync(AdditionalSectionModel sectionModel)
        {
            var section = JsonConvert.SerializeObject(sectionModel.Section);

            var messageContetn = string.Empty;
            string sectionResponse = await retryPolicy.ExecuteSectionWithRetryAsync(() => openAiService.CreateSectionAsync(sectionModel.InitialInputs!, section, messageContetn));

            if (string.IsNullOrEmpty(sectionResponse))
            {
                return null;
            }

            var sectionContent = JsonConvert.DeserializeObject<SectionContent>(sectionResponse);

            string html = sectionContent!.HTML;
            if (html.Contains("img"))
            {
                var parsedSection = await SharedFunctions.HandleSectionImageTags(sectionContent, retryPolicy, openAiService);

                return parsedSection;
            }

            return sectionContent;
        }
    }
}
