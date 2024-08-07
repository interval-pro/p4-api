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
        private readonly IConfiguration configuration;
        public OpenAiService(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }

        public async Task<string> CreateImageFromTextAsync(string question)
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

        public async Task<string> CreateLayoutAsync(string question)
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
                MaxTokens = 4000,
                ResponseFormat = new { type = "json_object" },
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
            string result = completionResponse.Choices?[0]?.Message?.Content!;

            return result;
        }

        public async Task<string> CreateSectionAsync(string question, string section, string messageContent)
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
                MaxTokens = 4000,
                ResponseFormat = new { type = "json_object" },
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

            if (!string.IsNullOrEmpty(messageContent))
            {
                var message = new Message()
                {
                    Role = "system",
                    Content = messageContent
                };

                completionRequest.Messages.Insert(2, message);
            }

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

            string result = completionResponse.Choices?[0]?.Message?.Content!;
            return result;
        }
    }
}
