namespace PageBuilder.Core.Constants
{
    public static class HeaderStyles
    {
        public static string HeaderStyle1 = @"
              @import url(""https://fonts.googleapis.com/css2?family=Roboto&family=Raleway&family=Ubuntu&family=Oswald&display=swap"");
              #header{
                width: 100%;
                height: auto;
                display: flex;
                justify-content: space-between;
                }
                #header nav ul{
                list-style-type: none;
                display: flex;
                justify-content: flex-end;
                gap: 5px;
                }
                #header nav ul li a{
                text-decoration: none;
                font-family: ""Roboto"";
                font-size: large;
                font-weight: 400;
                background-color: transparent;
                color: darkslategray;
                padding: 8px;
                border: 2px solid darkslategray;
                border-radius: 0px 7px;
    
                }
                #header nav ul li a:hover{
                background-color: darkslategray;
                color: white;
                }
                #header #logo {
                width: 160px;
                height: 110px;
                object-fit: contain;
                border-radius: 0px 7px;
                position: relative;
                top: 2em;
                left: 2em;
                    }";

        public static string HeaderStyle2 = @"@import url(""""https://fonts.googleapis.com/css2?family=Roboto&family=Raleway&family=Ubuntu&family=Oswald&display=swap"""");
              #header{
                width: 100%;
                height: auto;
                display: flex;
                justify-content: flex-start;
                }
                #header nav ul{
                list-style-type: none;
                display: flex;
                justify-content: flex-end;
                gap: 5px;
                }
                #header nav ul li a{
                text-decoration: none;
                font-family: """"Raleway"""";
                font-size: large;
                font-weight: 400;
                background-color: transparent;
                color: darkslategray;
                padding: 8px;
                border: 2px solid darkslategray;
                border-radius: 0px 0px;
    
                }
                #header nav ul li a:hover{
                background-color: darkslategray;
                color: white;
                }
                #header #logo {
                width: 160px;
                height: 110px;
                object-fit: contain;
                border-radius: 0px 0px;
                position: relative;
                top: 2em;
                left: 2em;
                    }""";
        public static string HeaderStyle3 = @"@import url(""https://fonts.googleapis.com/css2?family=Roboto&family=Raleway&family=Ubuntu&family=Oswald&display=swap"");
              #header{
                width: 100%;
                height: auto;
                display: flex;
                justify-content: space-around;
                }
                #header nav ul{
                list-style-type: none;
                display: flex;
                justify-content: flex-end;
                gap: 5px;
                }
                #header nav ul li a{
                text-decoration: none;
                font-family: ""Ubuntu"";
                font-size: small;
                font-weight: 400;
                background-color: transparent;
                color: black;
                padding: 8px;
                border: 2px solid black;
                border-radius: 20px;
    
                }
                #header nav ul li a:hover{
                background-color: black;
                color: white;
                }
                #header #logo {
                width: 160px;
                height: 110px;
                object-fit: contain;
                border-radius: 20px;
                position: relative;
                top: 2em;
                right: 2em;
                    }";
        //public const string HeaderStyle4 = @"st4";
        //public const string HeaderStyle5 = @"st5";
    }
}
