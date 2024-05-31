using Microsoft.Extensions.Configuration;
using System.Drawing.Imaging;

namespace PageBuilder.Core.Contracts
{
    public interface IOpenAiService
    {
        Task<string> GetChatCompletionAsync(IConfiguration conf, string question);
        Task<string> CreateImageFromTextAsync(IConfiguration conf, string question);
        Task<string> CreateLayoutAsync(IConfiguration configuration, string question);
        Task<string> CreateSectionAsync(IConfiguration configuration, string question, string section);
        void SaveImage(string imageUrl, string filename, ImageFormat format);
    }
}
