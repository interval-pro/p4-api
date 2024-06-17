namespace PageBuilder.Core.Models.ComponentModels
{
    public class AdditionalSectionModel
    {
        public string? InitialInputs { get; set; }

        public SectionModel Section { get; set; } = null!;
    }
}
