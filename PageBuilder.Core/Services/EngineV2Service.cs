using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PageBuilder.Core.Contracts;
using PageBuilder.Core.Models;
using PageBuilder.Core.Models.ComponentModels;
using static PageBuilder.Core.Constants.HeaderStyles;
using static PageBuilder.Core.Constants.HeroStyles;
using static PageBuilder.Core.Models.ChatGPT;

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
            int lastElement = 0, randomStyleIndex = 0;

            switch (targetSectionId)
            {
                case "header":
                    string[] headerStyles = {HeaderStyle1, HeaderStyle2, HeaderStyle3 };
                    lastElement = headerStyles.Count() - 1;
                    randomStyleIndex = new Random().Next(0, lastElement);
                    CssStyle = headerStyles[randomStyleIndex];
                    break;
                case "hero":
                    string[] heroStyles = { HeroStyle1, HeroStyle2, HeroStyle3 };
                    lastElement = heroStyles.Count() - 1;
                    randomStyleIndex = new Random().Next(0, lastElement);
                    CssStyle = heroStyles[randomStyleIndex];
                    break;
            }

            Message styleMessage = new()
            {
                Role = "system",
                Content = $"For {targetSectionId} use exactly this CSS style without any changes: " + CssStyle
            };

            var section = JsonConvert.SerializeObject(sectionModel);

            string sectionResponse = await retryPolicy.ExecuteSectionWithRetryAsync(() => openAiService.CreateSectionAsync(configuration, sectionModel.InitialInputs, section, styleMessage));

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
