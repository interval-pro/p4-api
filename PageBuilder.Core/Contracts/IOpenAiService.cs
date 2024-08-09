namespace PageBuilder.Core.Contracts
{
    public interface IOpenAiService
    {
        Task<string> CreateImageFromTextAsync(string question);

        Task<string> CreateLayoutAsync(string question);

        Task<string> CreateSectionAsync(string question, string section, string messageContent);
    }
}
