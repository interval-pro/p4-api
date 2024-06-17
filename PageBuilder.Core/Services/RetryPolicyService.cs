using Newtonsoft.Json.Linq;
using Polly;
using Polly.Retry;
using PageBuilder.Core.Contracts;
using static PageBuilder.Core.Constants.GeneralConstants;

namespace PageBuilder.Core.Services
{
    public class RetryPolicyService : IRetryPolicyService
    {
        private readonly AsyncRetryPolicy<string> layoutRetryPolicy;
        private readonly AsyncRetryPolicy<string> sectionRetryPolicy;
        private readonly AsyncRetryPolicy<string> imageRetryPolicy;

        public RetryPolicyService()
        {
            layoutRetryPolicy = Policy.HandleInner<HttpRequestException>()
            .OrResult<string>(res => !IsValidLayout(res))
            .WaitAndRetryAsync(
                retryCount: 5,
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Min(RetryExecutionDurationInSeconds, Math.Pow(2, attempt))));

            sectionRetryPolicy = Policy.HandleInner<HttpRequestException>()
                .OrResult<string>(res => !IsValidSection(res))
                .WaitAndRetryAsync(
                   retryCount: 5,
                   sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Min(RetryExecutionDurationInSeconds, Math.Pow(2, attempt))));

            imageRetryPolicy = Policy.HandleInner<HttpRequestException>()
                .OrResult<string>(res => !IsValidImage(res))
                .WaitAndRetryAsync(
                   retryCount: 5,
                   sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Min(RetryExecutionDurationInSeconds, Math.Pow(2, attempt))));
        }

        public async Task<string> ExecuteImageWithRetryAsync(Func<Task<string>> action)
        {
            var result = await imageRetryPolicy.ExecuteAsync(action);

            if (!IsValidImage(result))
            {
                return string.Empty;
            }

            return result;
        }

        public async Task<string> ExecuteLayoutWithRetryAsync(Func<Task<string>> action)
        {
            var result = await layoutRetryPolicy.ExecuteAsync(action);

            if (!IsValidLayout(result))
            {
                return string.Empty;
            }

            return result;
        }

        public async Task<string> ExecuteSectionWithRetryAsync(Func<Task<string>> action)
        {
            var result = await sectionRetryPolicy.ExecuteAsync(action);

            if (!IsValidSection(result))
            {
                return string.Empty;
            }

            return result;
        }

        private bool IsValidImage(string result)
        {
            if (string.IsNullOrWhiteSpace(result))
            {
                return false;
            }

            return true;
        }

        private bool IsValidLayout(string result)
        {
            if (string.IsNullOrEmpty(result))
            {
                return false;
            }

            try
            {
                var formatetResult = JObject.Parse(result);

                if (formatetResult["inputs"] == null || formatetResult["mainStyle"] == null || formatetResult["sections"] == null)
                {
                    return false;
                }

                var sections = formatetResult["sections"];
                if (sections == null || !sections.Any())
                {
                    return false;
                }

                foreach (var section in sections)
                {
                    if (section["sectionId"] == null || section["components"] == null)
                    {
                        return false;
                    }

                    foreach (var component in section["components"])
                    {
                        if (component["componentId"] == null || component["type"] == null || component["content"] == null)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        private bool IsValidSection(string result)
        {
            if (string.IsNullOrEmpty(result))
            {
                return false;
            }

            try
            {
                var formatetResult = JObject.Parse(result);

                if (formatetResult["HTML"] == null || formatetResult["CSS"] == null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
