namespace PageBuilder.Core.Constants
{
    public static class InstructionsToOpenAI
    {
        public const string botRole = @"You are a Front-end Web Developer. 
        Your task is to assist me in building an aesthetically pleasing landing page.";

        public const string botLayoutInstructions = @"Instructions:
        If there is image for generating please use GIFP(""__human readable text describing the image - prompt for image__"")
        At least 5 sections. Please Respond in fallowing json format:
        {""inputs"":""__inputs_from_prompt__ as human readable text""
        ""mainStyle:""__CSS_style_for_body_and_html_tags_in_css_code__""
        ""sections"": [{sectionId: ""header"", sectionId: ""hero"", ...
        ""components: [{componentId: ""logo"",  type: ""image"", content: ""GIFP(""..."")""},
        ""{componentId: ""navbar"", type: ""..."", content: ""5 links: Home, about us, ....""},... ]}, ...]}
        For content just use string, no array.";

        public const string botSectionInstructions = @"Instructions:  
        Use provided Section Information as raw simple referance and generate detailed section content. 
        Use input as main referance for all sections.   
        You can use any css properties.
        Procide only HTML and CSS style for current section.
        Use exactly this structure for section Header: 
        <header id='header'>
        <img id='logo'>
        <nav><ul><li><a></a></li></ul></nav>
        </header>
        Use exactly this structure for section Hero: <section id='hero'><img class='hero-bg'></img><div class='hero-content'><h1></h1><p></p><button></button></div></section>
        Please Respond in the following JSON format:
        { ""HTML"": ""<_section_tag_></_section_tag_>"" , ""CSS"": ""_the_css_style_"" }
        Generete it as detailed as possible.";
    }
}
