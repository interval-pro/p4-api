namespace PageBuilder.Core.Contracts
{
    public interface IRetryPolicyService
    {
        Task<string> ExecuteLayoutWithRetryAsync(Func<Task<string>> action);
        Task<string> ExecuteSectionWithRetryAsync(Func<Task<string>> action);
        Task<string> ExecuteImageWithRetryAsync(Func<Task<string>> action);
    }
}
