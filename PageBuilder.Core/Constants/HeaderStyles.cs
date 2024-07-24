namespace PageBuilder.Core.Constants
{
    public static class HeaderStyles
    {
        public static string HeaderStyle1 = @"
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
            object-fit: cover;
            border-radius: 0px 7px;
            position: relative;
            left: 2em;
        }";

        public static string HeaderStyle2 = @"
              #header {
            width: 97%;
            height: 5em;
            opacity: 0.8;
            display: flex;
            justify-content: flex-start;
            align-items: center;
            position: absolute;
            background-color: transparent;
            margin: 10px;
            padding: 10px;
            border-radius: 0px 7px;
            gap: 20px;
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
            font-family: 'Ubuntu';
            font-size: large;
            transition: font-size 0.5s;
            font-weight: 400;
            background-color: transparent;
            color: white;
            padding: 8px;
            border: 0px solid transparent;
            border-radius: 5%;
        }

        #header nav ul li a:hover {
            color: white;
            font-size: xx-large;
        }

        #header #logo {
            width: 160px;
            height: 110px;
            object-fit: cover;
            border-radius: 5%;
            position: relative;
            left: 2em;
        }";
        public static string HeaderStyle3 = @"
              #header {
            width: 97%;
            height: 5em;
            opacity: 0.8;
            display: flex;
            justify-content: center;
            align-items: center;
            position: absolute;
            background-color: transparent;
            margin: 10px;
            padding: 10px;
            border-radius: 7px 7px;
            border: 0px solid whitesmoke;
            border-width: 3px 0px 3px 0px;
            gap: 20px;
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
            font-family: 'Oswald';
            font-size: large;
            font-weight: 400;
            background-color: transparent;
            color: white;
            padding: 8px;
            border: 0px solid transparent;
            border-radius: 5%;
        }

        #header nav ul li a:hover {
            color: white;
            text-decoration: underline;
        }

        #header #logo {
            width: 160px;
            height: 110px;
            transition: font-size 0.5s;
            object-fit: cover;
            border-radius: 5%;
            position: relative;
            left: 2em;
        }";
        //public const string HeaderStyle4 = @"st4";
        //public const string HeaderStyle5 = @"st5";
    }
}
