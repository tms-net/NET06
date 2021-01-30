using System;
using APILibrary;

namespace UILibrary
{
    public class UIApplication
    {
        public void ToDo()
        {
            while (true)
            {
                var commandLine = Console.ReadLine();

                //int.TryParse(command, out var numberOfPeople)
                Command command = new Command(commandLine);
                switch (command.Name)
                {
                    case "list":

                        Console.WriteLine("You selected 10 currency");
                        APIClient aPIClient = new APIClient();

                        //List<Currency> currencyList = aPIClient.GetInfo(command.Param); // api группа делает привязку  на public void GetInfo ()
                        break;

                    
                    default:
                       
                        break;
                }
            }
        }
    }
}
