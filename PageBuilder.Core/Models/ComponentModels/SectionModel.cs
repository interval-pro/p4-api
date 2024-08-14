namespace PageBuilder.Core.Models.ComponentModels
{
    public class SectionModel
    {
        public string SectionId { get; set; } = string.Empty;
        public List<ComponentModel> Components { get; set; } = new List<ComponentModel>();
    }
}
