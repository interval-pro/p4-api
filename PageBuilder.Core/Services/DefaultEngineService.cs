using Microsoft.Extensions.Configuration;
using PageBuilder.Core.Contracts;
using PageBuilder.Core.Models;

namespace PageBuilder.Core.Services
{
    public class DefaultEngineService : IEngineService
    {
        private readonly IConfiguration configuration;
        private readonly string aiApiKey;

        public DefaultEngineService(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.aiApiKey = configuration["aiApiKey"];
        }

        public async Task<string> GeneratePageAsync(CreatePageModel jsonRequest)
        {
            var apiKey = aiApiKey;

            var result = jsonRequest.Input;

            // To Do: logic to generate the page with DefaultEngine

            return result;
        }

        public async Task<string> UpdatePageAsync(UpdatePageModel updateData, CreatePageModel currentData)
        {
            // To Do: logic to update the page with DefaultEngine

            currentData.Input = updateData.Input;

            return currentData.Input;
        }
    }
}
