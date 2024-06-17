namespace PageBuilder.Core.Models.ComponentModels
{
    public class LayoutModel
    {
        public string Inputs { get; set; } = string.Empty;

        public string MainStyle { get; set; } = string.Empty;

        public List<SectionModel> Sections { get; set; } = new List<SectionModel>();
    }
}