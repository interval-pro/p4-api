using System.Text.Json.Serialization;

namespace PageBuilder.Core.Models
{
    public class SectionContent
    {
        [JsonPropertyName("HTML")]
        public string HTML { get; set; } = string.Empty;
        [JsonPropertyName("CSS")]
        public string CSS { get; set; } = string.Empty;
    }
}
