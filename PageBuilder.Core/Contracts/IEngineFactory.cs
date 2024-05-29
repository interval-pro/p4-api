using PageBuilder.Core.Enums;

namespace PageBuilder.Core.Contracts
{
    public interface IEngineFactory
    {
        IEngineService GetEngine(EngineType engineType);
    }
}
