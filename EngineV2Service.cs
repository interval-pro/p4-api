using Microsoft.Extensions.Configuration;
using PageBuilder.Core.Contracts;
using PageBuilder.Core.Models;

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

        public Task<string> GenerateImageAsync(CreatePageModel jsonRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GeneratePageAsync(CreatePageModel jsonRequest)
        {
            var apiKey = aiApiKey;

            var result = jsonRequest.Input;

            // To Do: logic to generate the page with EngineV2

            return result;
        }

        public Task<string> ImageColorExtractAsync(CreatePageModel jsonRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UpdatePageAsync(UpdatePageModel updateData, CreatePageModel currentData)
        {
            // To Do: logic to update the page with EngineV2

            var result = string.Empty;  

            return result;
        }
    }
}
