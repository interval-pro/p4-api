using Microsoft.Extensions.Configuration;
using PageBuilder.Core.Contracts;
using PageBuilder.Core.Models;
using PageBuilder.Core.Models.ComponentModels;

namespace PageBuilder.Core.Services
{
    public class EngineV2Service : IEngineService
    {
        private readonly IConfiguration configuration;
        private readonly string aiApiKey;

        public EngineV2Service(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.aiApiKey = configuration["aiApiKey"];
        }

        public Task<string> GenerateImageAsync(CreateLayoutModel jsonRequest)
        {
            throw new NotImplementedException();
        }

        public Task<LayoutModel?> GenerateLayoutAsync(CreateLayoutModel jsonRequest)
        {
            var apiKey = aiApiKey;

            // To Do: logic to generate the page with EngineV2

            return null;
        }

        public Task<SectionContent?> GenerateSectionAsync(AdditionalSectionModel sectionModel)
        {
            throw new NotImplementedException();
        }

        public Task<string> ImageColorExtractAsync(CreateLayoutModel jsonRequest)
        {
            throw new NotImplementedException();
        }
    }
}
