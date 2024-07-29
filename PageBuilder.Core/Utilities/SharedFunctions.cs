using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using PageBuilder.Core.Contracts;
using PageBuilder.Core.Models;
using PageBuilder.Core.Models.ComponentModels;

namespace PageBuilder.Core.Utilities
{
    public static class SharedFunctions
    {
        public static async Task<SectionContent> HandleSectionImageTags(
           SectionContent sectionContent,
           IRetryPolicyService retryPolicy,
           IOpenAiService openAiService,
           IConfiguration configuration)
        {
            var html = sectionContent.HTML;

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var htmlNodes = htmlDocument.DocumentNode.SelectNodes("//img");

            if (!htmlNodes.Any())
            {
                return sectionContent;
            }

            for (int i = 0; i < htmlNodes.Count; i++)
            {
                var htmlNode = htmlNodes[i];
                var imageDescription = htmlNode.Attributes["src"].Value;

                var imageUrl = await retryPolicy.ExecuteImageWithRetryAsync(
                    () => openAiService.CreateImageFromTextAsync(configuration, imageDescription));

                if (string.IsNullOrEmpty(imageUrl))
                {
                    imageUrl = @"https://dummyimage.com/1792x1024/fff5";
                }

                htmlNode.Attributes["src"].Value = imageUrl;

                using (StringWriter writer = new StringWriter())
                {
                    htmlDocument.Save(writer);
                    sectionContent.HTML = writer.ToString();
                }
            }

            return sectionContent;
        }

        public static LayoutModel? ParseLayout(string layout, string input)
        {
            var obj = JObject.Parse(layout);

            if (obj != null)
            {
                var layoutModel = new LayoutModel()
                {
                    Inputs = input,
                    MainStyle = obj["mainStyle"]?.ToString(),
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
