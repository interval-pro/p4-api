using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PageBuilder.Core.Contracts;
using PageBuilder.Core.Models;

namespace PageBuilder.Core.Services
{
    public class DefaultEngineService : IEngineService
    {
        private readonly IConfiguration configuration;
        private readonly IOpenAiService openAiService;

        public DefaultEngineService(
            IConfiguration configuration,
            IOpenAiService openAiService)
        {
            this.configuration = configuration;
            this.openAiService = openAiService;
        }

        public async Task<string> GeneratePageAsync(CreatePageModel jsonRequest)
        {
            var input = jsonRequest.Input;

            //Create layout in json
            string layout = string.Empty;
            try
            {
                layout = await openAiService.CreateLayoutAsync(configuration, input);
            }
            catch (Exception x)
            {
                return x.Message;
            }
            //---- END ----

            //Deserialize layout
            JObject outObject = null!;
            try
            {
                outObject = JObject.Parse(layout);
            }
            catch (Exception x)
            {
                return x.Message;
            }
            var layoutModel = new
            {
                Inputs = outObject["inputs"].ToString(),
                MainStyle = outObject["mainStyle"].ToString(),
                SectionsNames = outObject["sections"].Values("sectionId")
                .Select(x => x.ToString())
                .ToList(),
                Sections = outObject["sections"]
                .Select(x => x.ToString())
                .ToList(),
                Components = outObject["sections"].Values("components").Values()
                .Select(x => x.ToString())
                .ToList()
            };
            //---- END ----

            //Create HTML & CSS for all sections
            string section = string.Empty;
            PageContent finalResult = new PageContent() { globalStyle = layoutModel.MainStyle };
            for (int i = 0; i < layoutModel.Sections.Count; i++)
            {
                section = layoutModel.Sections[i];
                string sectionResponse = string.Empty;
                try
                {
                    sectionResponse = await openAiService.CreateSectionAsync(configuration, input, section);
                }
                catch (Exception x)
                {
                    return x.Message;
                }
                SectionContent? jsonObject = JsonConvert.DeserializeObject<SectionContent>(sectionResponse);
                finalResult.sections.Add(new SectionContent() { HTML = jsonObject.HTML, CSS = jsonObject.CSS});
            }
            //---- END ----

            return JsonConvert.SerializeObject(finalResult);
        }

        public async Task<string> UpdatePageAsync(UpdatePageModel updateData, CreatePageModel currentData)
        {
            // To Do: logic to update the page with DefaultEngine

            currentData.Input = updateData.Input;

            return currentData.Input;
        }
    }
}
