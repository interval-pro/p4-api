using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PageBuilder.Core.Contracts;
using PageBuilder.Core.Models;
using PageBuilder.Core.Models.ComponentModels;


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

        public async Task<string> GenerateImageAsync(CreateLayoutModel jsonRequest)
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

            return JsonConvert.SerializeObject(new { url = imageUrl });
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

            LayoutModel? layoutModel = ParseLayout(layout, input);

            return layoutModel;
        }
  
        public async Task<SectionContent?> GenerateSectionAsync(AdditionalSectionModel sectionModel)
        {
            var section = JsonConvert.SerializeObject(sectionModel);

            string sectionResponse = await retryPolicy.ExecuteSectionWithRetryAsync(() => openAiService.CreateSectionAsync(configuration, sectionModel.InitialInputs, section));

            if (string.IsNullOrEmpty(sectionResponse))
            {
                return null;
            }

            var result = JsonConvert.DeserializeObject<SectionContent>(sectionResponse);

            return result;
        }

        public Task<string> ImageColorExtractAsync(CreateLayoutModel jsonRequest)
        {
            throw new NotImplementedException();
        }

        private static LayoutModel? ParseLayout(string layout, string input)
        {
            var obj = JObject.Parse(layout);

            if (obj != null)
            {
                var layoutModel = new LayoutModel()
                {
                    Inputs = input,
                    MainStyle = obj["mainStyle"]?.ToString(),
                    Sections = obj["sections"]?.Select(s => new SectionModel()
                    {
                        SectionId = s["sectionId"]?.ToString(),
                        Components = s["components"]?.Select(c => new ComponentModel()
                        {
                            ComponentId = c["componentId"]?.ToString(),
                            Type = c["type"]?.ToString(),
                            Content = c["content"]?.ToString()
                        }).ToList()
                    }).ToList()
                };

                return layoutModel;
            }

            return null;
        }
    }
}
