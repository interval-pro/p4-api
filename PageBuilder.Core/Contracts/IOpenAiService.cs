using Microsoft.Extensions.Configuration;

namespace PageBuilder.Core.Contracts
{
    public interface IOpenAiService
    {
        Task<string> CreateImageFromTextAsync(IConfiguration conf, string question);

        Task<string> CreateLayoutAsync(IConfiguration configuration, string question);

        Task<string> CreateSectionAsync(IConfiguration configuration, string question, string section);
    }
}
