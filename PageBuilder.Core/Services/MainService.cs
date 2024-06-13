using PageBuilder.Core.Contracts;
using PageBuilder.Core.Models;

namespace PageBuilder.Core.Services
{
    public class MainService : IMainService
    {
        private readonly IEngineFactory engineFactory;

        public MainService(IEngineFactory engineFactory)
        {
            this.engineFactory = engineFactory;
        }

        public async Task<string> GenerateImageAsync(CreatePageModel jsonRequest)
        {
            var engine = engineFactory.GetEngine(jsonRequest.EngineType);
            if (engine == null)
            {
                throw new ArgumentNullException("The engine can not be null!");
            }

            var result = await engine.GenerateImageAsync(jsonRequest);

            return result;
        }

        public async Task<string> GeneratePageAsync(CreatePageModel jsonRequest)
        {
            var engine = engineFactory.GetEngine(jsonRequest.EngineType);
            if (engine == null)
            {
                throw new ArgumentNullException("The engine can not be null!");
            }

            var result = await engine.GeneratePageAsync(jsonRequest);

            return result;
        }

        public async Task<string> ImageColorExtractAsync(CreatePageModel jsonRequest)
        {
            var engine = engineFactory.GetEngine(jsonRequest.EngineType);
            if (engine == null)
            {
                throw new ArgumentNullException("The engine can not be null!");
            }

            var result = await engine.ImageColorExtractAsync(jsonRequest);

            return result;
        }

        public async Task<string> UpdatePageAsync(UpdatePageModel updateData, CreatePageModel currentData)
        {
            //To Do

            var engine = engineFactory.GetEngine(updateData.EngineType);
            if (engine == null)
            {
                throw new ArgumentNullException("The engine can not be null!");
            }

            var result = await engine.UpdatePageAsync(updateData, currentData);

            return result;
        }
    }
}
