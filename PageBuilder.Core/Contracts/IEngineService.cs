using PageBuilder.Core.Models;
using PageBuilder.Core.Models.ComponentModels;

namespace PageBuilder.Core.Contracts
{
    public interface IEngineService
    {
        Task<object> GenerateImageAsync(CreateLayoutModel jsonRequest);
        
        Task<LayoutModel?> GenerateLayoutAsync(CreateLayoutModel inputs);

        Task<SectionContent?> GenerateSectionAsync(AdditionalSectionModel sectionModel);

        Task<string> ImageColorExtractAsync(CreateLayoutModel jsonRequest);
    }
}
