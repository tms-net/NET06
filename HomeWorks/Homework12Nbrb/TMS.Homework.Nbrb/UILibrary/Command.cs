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
           string[] NameParamArray = commandLine.Split('-');

           name = NameParamArray[0];
            if (NameParamArray.Length == 2) param = NameParamArray[1]; else param = "";
          
        }

    }

}
