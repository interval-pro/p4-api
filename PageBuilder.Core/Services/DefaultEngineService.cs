using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PageBuilder.Core.Contracts;
using PageBuilder.Core.Models;
using PageBuilder.Core.Models.ComponentModels;
using static PageBuilder.Core.Models.ChatGPT;
using HtmlAgilityPack;


namespace PageBuilder.Core.Services
{
    public class DefaultEngineService : IEngineService
    {
        private readonly IConfiguration configuration;
        private readonly IOpenAiService openAiService;
        private readonly IRetryPolicyService retryPolicy;

        public DefaultEngineService(
            IConfiguration configuration,
            IOpenAiService openAiService,
            IRetryPolicyService retryPolicy)
        {
            this.configuration = configuration;
            this.openAiService = openAiService;
            this.retryPolicy = retryPolicy;
        }

        public async Task<object> GenerateImageAsync(CreateLayoutModel jsonRequest)
        {
            var input = jsonRequest.Inputs;
            if (string.IsNullOrWhiteSpace(input))
            {
                return "Invalid input.";
            }

            string imageUrl = string.Empty;

            //Generate Image with DALL-E-3
            try
            {
                imageUrl = await retryPolicy.ExecuteImageWithRetryAsync(() => openAiService.CreateImageFromTextAsync(configuration, input));

                if (string.IsNullOrEmpty(imageUrl))
                {
                    return "Failed to generate image";
                }
            }
            catch (Exception x)
            {
                return x.Message;
            }
            //---- END ----

            return new { url = imageUrl };
        }

        public async Task<LayoutModel?> GenerateLayoutAsync(CreateLayoutModel request)
        {
            var input = request.Inputs;

            string layout = string.Empty;
            try
            {
                layout = await retryPolicy.ExecuteLayoutWithRetryAsync(() => openAiService.CreateLayoutAsync(configuration, input));

                if (string.IsNullOrEmpty(layout))
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }

            LayoutModel? layoutModel = ParseLayout(layout, input);

            return layoutModel;
        }
  
        public async Task<SectionContent?> GenerateSectionAsync(AdditionalSectionModel sectionModel)
        {
            //Get all image descriptions and generate image/images for every component
            string text = string.Join(", ", sectionModel.Section.Components.Select(x => x.Content).ToList());
            List<string> imageSources = new List<string>();
            while (text.Contains("GIFP"))
            {
                int startIndex = text.IndexOf("GIFP");
                int endIndex = text.IndexOf(")") + 1;
                string description = text.Substring(startIndex, endIndex - startIndex);
                string imageSource = string.Empty;

                try
                {
                    imageSource = await retryPolicy.ExecuteImageWithRetryAsync(() => openAiService.CreateImageFromTextAsync(configuration, description));
                    
                    if (string.IsNullOrEmpty(imageSource))
                    {
                        imageSource = @"https://dummyimage.com/1792x1024/fff5";
                    }
                }
                catch
                {

                }
                imageSources.Add(imageSource);
                text = text.Replace(description, "");
            }
            //---- END ----

            Message styleMessage = new()
            {
                Role = "system",
                Content = ""
            };

            var section = JsonConvert.SerializeObject(sectionModel.Section);

            string sectionResponse = await retryPolicy.ExecuteSectionWithRetryAsync(() => openAiService.CreateSectionAsync(configuration, sectionModel.InitialInputs, section, styleMessage));

            if (string.IsNullOrEmpty(sectionResponse))
            {
                return null;
            }

            var sectionContent = JsonConvert.DeserializeObject<SectionContent>(sectionResponse);
            string html = sectionContent.HTML;

            //Add generated image source to the right place
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var htmlNodes = htmlDocument.DocumentNode.SelectNodes("//img");
            if (htmlNodes != null)
            {
                for (int i = 0; i < htmlNodes.Count; i++)
                {
                    var htmlNode = htmlNodes[i];
                    var imgSrc = imageSources[i];
                    
                    htmlNode.Attributes["src"].Value = imgSrc;

                    using (StringWriter writer = new StringWriter())
                    {
                        htmlDocument.Save(writer);
                        sectionContent.HTML = writer.ToString();
                    }
                }
            }
            //---- END ----

            return sectionContent;
        }

        public Task<string> ImageColorExtractAsync(CreateLayoutModel jsonRequest)
        {
            throw new NotImplementedException();
        }

        private static LayoutModel? ParseLayout(string layout, string input)
        {
            string importGoolgeFonts = @"@import url('https://fonts.googleapis.com/css2?family=Roboto&family=Raleway&family=Ubuntu&family=Oswald&display=swap');";

            var obj = JObject.Parse(layout);

            if (obj != null)
            {
                var layoutModel = new LayoutModel()
                {
                    Inputs = input,
                    MainStyle = importGoolgeFonts + "  " + obj["mainStyle"]?.ToString(),
                    Sections = obj["sections"]?.Select(s => new SectionModel()
                    {
                        SectionId = s["sectionId"]?.ToString(),
                        Components = s["components"]?.Select(c => new ComponentModel()
                        {
                            ComponentId = c["componentId"]?.ToString(),
                            Type = c["type"]?.ToString(),
                            Content = c["content"]?.ToString()
                        }).ToList()
                    }).ToList()
                };

                return layoutModel;
            }

            return null;
        }
    }
}
