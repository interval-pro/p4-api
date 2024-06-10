using System.Text.Json.Serialization;

namespace PageBuilder.Core.Models
{
    public class PageContent
    {
        [JsonPropertyName("globalStyle")]
        public string? globalStyle { get; set; }
        [JsonPropertyName("sections")]
        public ICollection<SectionContent> sections { get; set; } = new List<SectionContent>();
    }
}
