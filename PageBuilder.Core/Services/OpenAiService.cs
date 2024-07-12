using Microsoft.Extensions.Configuration;
using PageBuilder.Core.Contracts;
using System.Text;
using System.Text.Json;
using static PageBuilder.Core.Constants.InstructionsToOpenAI;
using static PageBuilder.Core.Models.ChatGPT;
namespace PageBuilder.Core.Services
{
    public class OpenAiService : IOpenAiService
    {
        public async Task<string> CreateImageFromTextAsync(IConfiguration configuration, string question)
        {
            IHttpClientFactory? httpClientFactory = null;
            HttpClient httpClient = new HttpClient();
            if (httpClientFactory != null)
            {
                httpClient = httpClientFactory.CreateClient("ChatGPT");
            }

            TextToImageRequest completionRequest = new()
            {
                Model = "dall-e-3",
                N = 1,
                Prompt = question,
                Size = "1792x1024",
                ResponseFormat = "url"
            };

            using var httpReq = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/images/generations");
            httpReq.Headers.Add("Authorization", $"Bearer {configuration["aiApiKey"]}");

            string requestString = JsonSerializer.Serialize(completionRequest);
            httpReq.Content = new StringContent(requestString, Encoding.UTF8, "application/json");

            using HttpResponseMessage? httpResponse = await httpClient.SendAsync(httpReq);
            httpResponse.EnsureSuccessStatusCode();

            var completionResponse = httpResponse.IsSuccessStatusCode ? JsonSerializer.Deserialize<TextToImageResponse>(await httpResponse.Content.ReadAsStringAsync()) : null;
            return completionResponse.Data?[0]?.Url;
        }

        public async Task<string> CreateLayoutAsync(IConfiguration configuration, string question)
        {
            IHttpClientFactory? httpClientFactory = null;
            HttpClient httpClient = new HttpClient();

            if (httpClientFactory != null)
            {
                httpClient = httpClientFactory.CreateClient("ChatGPT");
            }

            ChatCompletionRequest completionRequest = new()
            {
                Model = "gpt-4o",
                MaxTokens = 2000,
                Messages =
                {
                    new Message()
                    {
                        Role = "system",
                        Content = botRole
                    },
                    new Message()
                    {
                        Role = "system",
                        Content = botLayoutInstructions
                    },
                    new Message()
                    {
                        Role = "user",
                        Content = question
                    }
                }
            };

            using var httpReq = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
            httpReq.Headers.Add("Authorization", $"Bearer {configuration["aiApiKey"]}");

            string requestString = JsonSerializer.Serialize(completionRequest);
            httpReq.Content = new StringContent(requestString, Encoding.UTF8, "application/json");

            using HttpResponseMessage? httpResponse = await httpClient.SendAsync(httpReq);
            httpResponse.EnsureSuccessStatusCode();

            var completionResponse = httpResponse.IsSuccessStatusCode ? JsonSerializer.Deserialize<ChatCompletionResponse>(await httpResponse.Content.ReadAsStringAsync()) : null;
            string result = completionResponse.Choices?[0]?.Message?.Content;

            var startIndex = result.IndexOf('{');
            var endIndex = result.LastIndexOf("```");

            if (startIndex > -1 && endIndex > -1 && endIndex > startIndex)
            {
                return result.Substring(startIndex, endIndex - startIndex - 1);
            }

            return string.Empty;
        }

        public async Task<string> CreateSectionAsync(IConfiguration configuration, string question, string section, Message styleMessage)
        {
            IHttpClientFactory? httpClientFactory = null;
            HttpClient httpClient = new HttpClient();

            if (httpClientFactory != null)
            {
                httpClient = httpClientFactory.CreateClient("ChatGPT");
            }

            ChatCompletionRequest completionRequest = new()
            {
                Model = "gpt-4o",
                MaxTokens = 2000,
                Messages =
                {
                    new Message()
                    {
                        Role = "system",
                        Content = botRole
                    },
                    new Message()
                    {
                        Role = "system",
                        Content = botSectionInstructions
                    },
                    new Message()
                    {
                        Role = "user",
                        Content = question
                    },
                    new Message()
                    {
                        Role = "user",
                        Content = section
                    }
                }
            };

            completionRequest.Messages.Add(styleMessage);

            using var httpReq = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
            httpReq.Headers.Add("Authorization", $"Bearer {configuration["aiApiKey"]}");

            string requestString = JsonSerializer.Serialize(completionRequest);
            httpReq.Content = new StringContent(requestString, Encoding.UTF8, "application/json");

            using HttpResponseMessage? httpResponse = await httpClient.SendAsync(httpReq);
            httpResponse.EnsureSuccessStatusCode();

            var completionResponse = httpResponse.IsSuccessStatusCode ? JsonSerializer.Deserialize<ChatCompletionResponse>(await httpResponse.Content.ReadAsStringAsync()) : null;
            if (completionResponse == null)
            {
                return string.Empty;
            }

            string result = completionResponse.Choices?[0]?.Message?.Content;

            return result.Replace("```json", "").Replace("```", "");
        }
    }
}
