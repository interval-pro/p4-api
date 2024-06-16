using PageBuilder.Core.Models;
using PageBuilder.Core.Models.ComponentModels;

namespace PageBuilder.Core.Contracts
{
    public interface IEngineService
    {
        Task<string> GenerateImageAsync(CreateLayoutModel jsonRequest);
        
        Task<LayoutModel?> GenerateLayoutAsync(CreateLayoutModel inputs);

        Task<SectionContent?> GenerateSectionAsync(AdintionalSectionModel sectionModel);

        Task<string> ImageColorExtractAsync(CreateLayoutModel jsonRequest);
    }
}
