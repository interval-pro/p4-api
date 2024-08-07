namespace PageBuilder.Core.Models
{
    public class RegenerateImageModel
    {
        public string? Context { get; set; }

        public string Prompt { get; set; } = null!;
    }
}
