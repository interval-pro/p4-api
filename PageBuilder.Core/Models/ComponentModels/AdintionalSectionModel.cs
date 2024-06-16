namespace PageBuilder.Core.Models.ComponentModels
{
    public class AdintionalSectionModel
    {
        public string? InitialInputs { get; set; }

        public string SectionId { get; set; } = string.Empty;

        public List<ComponentModel> Components { get; set; } = new List<ComponentModel>();
    }
}
