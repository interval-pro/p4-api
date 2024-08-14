namespace PageBuilder.Core.Constants
{
    public static class HeaderStyles
    {
        public static string HeaderStyle1 = @"
              @import url(""https://fonts.googleapis.com/css2?family=Roboto&family=Raleway&family=Ubuntu&family=Oswald&display=swap"");
              #header {
            width: 95%;
            height: 5em;
            opacity: 0.8;
            display: flex;
            justify-content: space-between;
            position: absolute;
            background-color: darkslategrey;
            margin: 10px;
            padding: 10px;
            border-radius: 0px 7px;
            z-index: 1;
        }

        #header nav ul {
            position: relative;
            list-style-type: none;
            display: flex;
            justify-content: flex-end;
            gap: 5px;
        }

        #header nav ul li a {
            text-decoration: none;
            font-family: ""Ubuntu"";
            font-size: large;
            font-weight: 400;
            background-color: transparent;
            color: wheat;
            padding: 8px;
            border: 2px solid wheat;
            border-radius: 0px 7px;
        }

        #header nav ul li a:hover {
            background-color: wheat;
            color: black;
        }

        #header #logo {
            width: 160px;
            height: 110px;
            object-fit: contain;
            border-radius: 0px 7px;
            position: relative;
            left: 2em;
        }";

        public static string HeaderStyle2 = @"@import url(""""https://fonts.googleapis.com/css2?family=Roboto&family=Raleway&family=Ubuntu&family=Oswald&display=swap"""");
              #header {
            width: 95%;
            height: 5em;
            opacity: 0.8;
            display: flex;
            justify-content: space-between;
            position: absolute;
            background-color: darkslategrey;
            margin: 10px;
            padding: 10px;
            border-radius: 0px 7px;
            z-index: 1;
        }

        #header nav ul {
            position: relative;
            list-style-type: none;
            display: flex;
            justify-content: flex-end;
            gap: 5px;
        }

        #header nav ul li a {
            text-decoration: none;
            font-family: ""Raleway"";
            font-size: large;
            font-weight: 400;
            background-color: transparent;
            color: wheat;
            padding: 8px;
            border: 2px solid wheat;
            border-radius: 0px 7px;
        }

        #header nav ul li a:hover {
            background-color: wheat;
            color: black;
        }

        #header #logo {
            width: 160px;
            height: 110px;
            object-fit: contain;
            border-radius: 0px 7px;
            position: relative;
            left: 2em;
        }";
        public static string HeaderStyle3 = @"@import url(""https://fonts.googleapis.com/css2?family=Roboto&family=Raleway&family=Ubuntu&family=Oswald&display=swap"");
              #header {
            width: 95%;
            height: 5em;
            opacity: 0.8;
            display: flex;
            justify-content: space-between;
            position: absolute;
            background-color: darkslategrey;
            margin: 10px;
            padding: 10px;
            border-radius: 0px 7px;
            z-index: 1;
        }

        #header nav ul {
            position: relative;
            list-style-type: none;
            display: flex;
            justify-content: flex-end;
            gap: 5px;
        }

        #header nav ul li a {
            text-decoration: none;
            font-family: ""Oswald"";
            font-size: large;
            font-weight: 400;
            background-color: transparent;
            color: wheat;
            padding: 8px;
            border: 2px solid wheat;
            border-radius: 0px 7px;
        }

        #header nav ul li a:hover {
            background-color: wheat;
            color: black;
        }

        #header #logo {
            width: 160px;
            height: 110px;
            object-fit: contain;
            border-radius: 0px 7px;
            position: relative;
            left: 2em;
        }";
    }
}
