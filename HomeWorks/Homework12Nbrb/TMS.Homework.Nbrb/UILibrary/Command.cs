using System;
using System.Collections.Generic;
using System.Text;

namespace UILibrary
{
    class Command
    {
        public string Name;
        public string Param;

        public Command(string commandLine)

        {
            ParseCommandLine(commandLine, out Name, out Param);
        }

        private void ParseCommandLine(string commandLine, out string name, out string param)
        {
            string[] nameParamArray = commandLine.Split('-');

            if (nameParamArray.Length != 0)
            {
                name = nameParamArray[0].Trim().ToLower();
                if (nameParamArray.Length == 2) param = nameParamArray[1].Trim().ToLower(); else param = "";
            }
            else { name = ""; param = ""; }

        }

    }

}
