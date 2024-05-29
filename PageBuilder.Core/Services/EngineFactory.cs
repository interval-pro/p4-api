using Microsoft.Extensions.DependencyInjection;
using PageBuilder.Core.Contracts;
using PageBuilder.Core.Enums;

namespace PageBuilder.Core.Services
{
    public class EngineFactory : IEngineFactory
    {
        private readonly IServiceProvider serviceProvider;

        public EngineFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IEngineService GetEngine(EngineType engineType)
        {
            return engineType switch
            {
                EngineType.DefaultEngine => serviceProvider.GetRequiredService<DefaultEngineService>(),
                EngineType.EngineV2 => serviceProvider.GetRequiredService<EngineV2Service>(),
                _ => throw new ArgumentException($"Engine type {engineType} is not supported."),
            };
        }
    }
}
