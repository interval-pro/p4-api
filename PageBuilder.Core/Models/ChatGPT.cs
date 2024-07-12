using System.Text.Json.Serialization;

namespace PageBuilder.Core.Models
{
    public class ChatGPT
    {
        /// <summary>
        /// Create model for text response
        /// </summary>
        public class ChatCompletionRequest
        {
            [JsonPropertyName("model")]
            public string? Model { get; set; }
            [JsonPropertyName("messages")]
            public List<Message> Messages { get; set; } = new List<Message>();
            [JsonPropertyName("max_tokens")]
            public int MaxTokens { get; set; }

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

        /// <summary>
        /// Create model for image response (Dall-e-3)
        /// </summary>
        public class TextToImageRequest
        {
            [JsonPropertyName("model")]
            public string Model { get; set; } = null!;
            [JsonPropertyName("prompt")]
            public string Prompt { get; set; } = string.Empty;
            [JsonPropertyName("n")]
            public int N { get; set; } //You can request 1 image at a time with DALL·E 3 (request more by making parallel requests) or up to 10 images at a time using DALL·E 2
            [JsonPropertyName("size")]
            public string? Size { get; set; } //When using DALL·E 3, images can have a size of 1024x1024, 1024x1792 or 1792x1024 pixels.
            [JsonPropertyName("response_format")]
            public string? ResponseFormat { get; set; } //The format in which the generated images are returned. Must be one of url or b64_json. URLs are only valid for 60 minutes after the image has been generated.
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
