using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PageBuilder.Core.Contracts;
using PageBuilder.Core.Models;
using PageBuilder.Core.Models.ComponentModels;
using static PageBuilder.Core.Models.ChatGPT;
using static PageBuilder.Core.Constants.HeaderStyles;
using PageBuilder.Core.Constants;

namespace PageBuilder.Core.Services
{
    public class EngineV2Service : IEngineService
    {
        private readonly IConfiguration configuration;
        private readonly IOpenAiService openAiService;
        private readonly IRetryPolicyService retryPolicy;

        public EngineV2Service(
            IConfiguration configuration,
            IOpenAiService openAiService,
            IRetryPolicyService retryPolicy)
        {
            this.configuration = configuration;
            this.openAiService = openAiService;
            this.retryPolicy = retryPolicy;
        }

        public Task<object> GenerateImageAsync(CreateLayoutModel jsonRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<LayoutModel?> GenerateLayoutAsync(CreateLayoutModel jsonRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<SectionContent?> GenerateSectionAsync(AdditionalSectionModel sectionModel)
        {
            string CssStyle = string.Empty;
            string targetSectionId = sectionModel.Section.SectionId;

            switch (targetSectionId)
            {
                case "header":
                    string[] headerStyles = {HeaderStyle1, HeaderStyle2, HeaderStyle3 };
                    int lastElement = headerStyles.Count() - 1;
                    int randomStyleIndex = new Random().Next(0, lastElement);
                    CssStyle = headerStyles[randomStyleIndex];
                    break;
                case "hero":
                    break;
                case "footer":
                    break;
            }

            Message styleMessages = new()
            {
                Role = "system",
                Content = $"For {targetSectionId} use exactly this CSS style without any changes: " + CssStyle
            };

            var section = JsonConvert.SerializeObject(sectionModel);

            string sectionResponse = await retryPolicy.ExecuteSectionWithRetryAsync(() => openAiService.CreateSectionAsync(configuration, sectionModel.InitialInputs, section, styleMessages));

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
    }
}
