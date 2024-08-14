using System.Text.Json.Serialization;

namespace PageBuilder.Core.Models
{
    public class ChatGPT
    {
        public class ChatCompletionRequest
        {
            [JsonPropertyName("model")]
            public string? Model { get; set; }
            [JsonPropertyName("messages")]
            public List<Message> Messages { get; set; } = new List<Message>();
            [JsonPropertyName("max_tokens")]
            public int MaxTokens { get; set; }

            [JsonPropertyName("response_format")]
            public object ResponseFormat { get; set; }

        }

        public class Choice
        {
            [JsonPropertyName("index")]
            public int Index { get; set; }

            [JsonPropertyName("message")]
            public Message? Message { get; set; }

            [JsonPropertyName("logprobs")]
            public object? Logprobs { get; set; }

            [JsonPropertyName("finish_reason")]
            public string? FinishReason { get; set; }
        }

        public class Message
        {
            [JsonPropertyName("role")]
            public string Role { get; set; } = string.Empty;

            [JsonPropertyName("content")]
            public string Content { get; set; } = string.Empty;
        }

        public class ChatCompletionResponse
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("object")]
            public string Object { get; set; }

            [JsonPropertyName("created")]
            public int Created { get; set; }

            [JsonPropertyName("model")]
            public string Model { get; set; }

            [JsonPropertyName("choices")]
            public List<Choice> Choices { get; set; }

            [JsonPropertyName("usage")]
            public Usage Usage { get; set; }

            [JsonPropertyName("system_fingerprint")]
            public object SystemFingerprint { get; set; }
        }

        public class Usage
        {
            [JsonPropertyName("prompt_tokens")]
            public int PromptTokens { get; set; }

            [JsonPropertyName("completion_tokens")]
            public int CompletionTokens { get; set; }

            [JsonPropertyName("total_tokens")]
            public int TotalTokens { get; set; }
        }

        public class TextToImageRequest
        {
            [JsonPropertyName("model")]
            public string Model { get; set; } = null!;
            [JsonPropertyName("prompt")]
            public string Prompt { get; set; } = string.Empty;
            [JsonPropertyName("n")]
            public int N { get; set; }
            [JsonPropertyName("size")]
            public string? Size { get; set; }
            [JsonPropertyName("response_format")]
            public string? ResponseFormat { get; set; }
        }

        public class Data
        {
            [JsonPropertyName("revised_prompt")]
            public string? RevisedPrompt { get; set; }
            [JsonPropertyName("url")]
            public string? Url { get; set; }
        }

        public class TextToImageResponse
        {
            [JsonPropertyName("created")]
            public int Created { get; set; }
            [JsonPropertyName("data")]
            public List<Data> Data { get; set; } = new List<Data>();
        }
    }
}
