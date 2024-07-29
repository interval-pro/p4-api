using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PageBuilder.Core.Contracts;
using PageBuilder.Core.Models;
using PageBuilder.Core.Models.ComponentModels;
using PageBuilder.Core.Utilities;


namespace PageBuilder.Core.Services
{
    public class DefaultEngineService : IEngineService
    {
        private readonly IConfiguration configuration;
        private readonly IOpenAiService openAiService;
        private readonly IRetryPolicyService retryPolicy;

        public DefaultEngineService(
            IConfiguration configuration,
            IOpenAiService openAiService,
            IRetryPolicyService retryPolicy)
        {
            this.configuration = configuration;
            this.openAiService = openAiService;
            this.retryPolicy = retryPolicy;
        }

        public async Task<object> GenerateImageAsync(CreateLayoutModel jsonRequest)
        {
            var input = jsonRequest.Inputs;
            if (string.IsNullOrWhiteSpace(input))
            {
                return "Invalid input.";
            }

            string imageUrl = string.Empty;

            //Generate Image with DALL-E-3
            try
            {
                imageUrl = await retryPolicy.ExecuteImageWithRetryAsync(() => openAiService.CreateImageFromTextAsync(configuration, input));

                if (string.IsNullOrEmpty(imageUrl))
                {
                    return "Failed to generate image";
                }
            }
            catch (Exception x)
            {
                return x.Message;
            }
            //---- END ----

            return new { url = imageUrl };
        }

        public async Task<LayoutModel?> GenerateLayoutAsync(CreateLayoutModel request)
        {
            var input = request.Inputs;

            string layout = string.Empty;
            try
            {
                layout = await retryPolicy.ExecuteLayoutWithRetryAsync(() => openAiService.CreateLayoutAsync(configuration, input));

                if (string.IsNullOrEmpty(layout))
                {
                    return null;
                }
            }
            catch (Exception)
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
            string sectionResponse = await retryPolicy.ExecuteSectionWithRetryAsync(() => openAiService.CreateSectionAsync(configuration, sectionModel.InitialInputs, section, messageContetn));

            if (string.IsNullOrEmpty(sectionResponse))
            {
                return null;
            }

            var sectionContent = JsonConvert.DeserializeObject<SectionContent>(sectionResponse);

            string html = sectionContent!.HTML;
            if (html.Contains("img"))
            {
                var parsedSection = await SharedFunctions.HandleSectionImageTags(sectionContent, retryPolicy, openAiService, configuration);
               
                return parsedSection;
            }

            return sectionContent;
        }

        public Task<string> ImageColorExtractAsync(CreateLayoutModel jsonRequest)
        {
            throw new NotImplementedException();
        }
    }
}
