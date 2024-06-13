using PageBuilder.Core.Models;

namespace PageBuilder.Core.Contracts
{
    public interface IMainService
    {
        Task<string> GenerateImageAsync(CreatePageModel jsonRequest);
        
        Task<string> GeneratePageAsync(CreatePageModel jsonRequest);

        Task<string> ImageColorExtractAsync(CreatePageModel jsonRequest);

        Task<string> UpdatePageAsync(UpdatePageModel updateData, CreatePageModel currentData);
    }
}
