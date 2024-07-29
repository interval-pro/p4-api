using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PageBuilder.Core.Contracts;
using PageBuilder.Core.Models;
using PageBuilder.Core.Models.ComponentModels;
using PageBuilder.Core.Utilities;
using static PageBuilder.Core.Constants.HeaderStyles;
using static PageBuilder.Core.Constants.HeroStyles;

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
            string CssStyle = string.Empty;
            string targetSectionId = sectionModel.Section.SectionId;
            int lastElement = 0, randomStyleIndex = 0;

            switch (targetSectionId)
            {
                case "header":
                    string[] headerStyles = { HeaderStyle1, HeaderStyle2, HeaderStyle3 };
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
                default:
                    CssStyle = string.Empty;
                    break;
            }

            string messageContent = string.Empty;
            if (!string.IsNullOrEmpty(CssStyle))
            {
                messageContent = $"For {targetSectionId} use exactly this CSS style without any changes: {CssStyle}";
            }

            var section = JsonConvert.SerializeObject(sectionModel.Section);

            string sectionResponse = await retryPolicy.ExecuteSectionWithRetryAsync(() => openAiService.CreateSectionAsync(configuration, sectionModel.InitialInputs, section, messageContent));

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
