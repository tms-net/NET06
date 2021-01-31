using APILibrary;
using System;
using System.Collections.Generic;
using APILibrary.Models;

namespace UILibrary
{
    public class UIApplication
    {
        private List<ShortCurrency> currencyList; 
        private List<Rate> currencyExRates;
        public void ToDo()
        {
            Console.WriteLine("Hello! You are greeted by NbRb.");
            APIClient aPIClient = new APIClient();

            PrintHelp();

            while (true)
            {
                var commandLine = Console.ReadLine();

                Command command = new Command(commandLine);


                switch (command.Name)
                {
                    case "list":
                        int.TryParse(command.Param, out int currencyNumber);
                        if (currencyNumber > 0)
                        {
                            Console.WriteLine($"You selected {currencyNumber} currencies");
                        }
                        else
                        {
                            Console.WriteLine($"You selected all currencies");
                        }
                        Console.WriteLine("======================================");
                        currencyList = aPIClient.GetShortCurrencies(currencyNumber);
                        PrintCurrencies();

                        break;

                    case "exrate":
                        ShortCurrency selectedCurrensy = null;
                        Console.WriteLine("Enter currency code:");
                        int.TryParse(Console.ReadLine(), out int code);
                        if (currencyList!= null)
                        { 
                            if (currencyList.Count > 0)
                            {                           
                                selectedCurrensy = FindCurrencyInList(code);
                            }
                        }

                        if (selectedCurrensy == null)
                        {
                            //selectedCurrensy = aPIClient.FindCurrency(code);
                        }


                        if (selectedCurrensy != null)
                        {
                            if (currencyExRates != null) currencyExRates.Clear();
                            if (command.Param == "p")
                            {
                                InputDates(out DateTime date1, out DateTime date2);
                                List<Rate> currencyExRates = aPIClient.GetRates(date1, date2, selectedCurrensy.Code);
                            }
                            else
                            {
                                DateTime date = InputDate();
                                Rate currencyRate = aPIClient.GetRates(date, selectedCurrensy.Code);
                                currencyExRates.Add(currencyRate);
                            }

                            PrintCurrencyExrates(currencyExRates);
                        }

                        break;
                    case "save":
                        {
                            Console.WriteLine("Input path:");
                            string path = Console.ReadLine();
                            if (currencyExRates != null)
                            {
                                if (currencyExRates.Count > 0)
                                {
                                    //FileService.SaveInFile(path, currencyCourses); }
                                }
                                else Console.WriteLine("There is nothing to save!");
                            }
                            else Console.WriteLine("There is nothing to save!");
                            break;
                        }
                    case "exit":
                        return;

                    default:
                        PrintHelp();
                        break;
                }
            }
        }

        private ShortCurrency FindCurrencyInList(int code)
        {
            foreach (var currency in currencyList)
            {
                if (currency.Code == code) return currency;
            }

            return null;
        }

        private void InputDates(out DateTime date1, out DateTime date2)
        {
            date1 = InputDate("start");
            date2 = InputDate("end");
        }

        private DateTime InputDate(string nameOfDate = "")
        {
            bool isOk = false;
            DateTime date = new DateTime();

            while (!isOk)
            {
                Console.WriteLine("Enter {nameOfDate} date from (day-month-year): ");
                isOk = DateTime.TryParse(Console.ReadLine(), out date);

                if (!isOk)
                {
                    Console.WriteLine("Error!!! Wrong format ( dd-mm-yyyy)!!! Please Try again ");
                }
            }

            return date;
        }

        private void PrintCurrencyExrates(List<Rate> currencyExRates)
        {
        }

        private void PrintHelp()
        {
            Console.WriteLine("===========HELP=============");
            Console.WriteLine("list         display all currencies");
            Console.WriteLine("list -n      display n currencies (n - number of currencies)");
            Console.WriteLine("exrate       request&display currency rates on date");
            Console.WriteLine("exrate -p    request&display currency rates for the period");
            Console.WriteLine("save         save requested currency rates");
            Console.WriteLine("exit         exit program");
            Console.WriteLine("help         display help");
            Console.WriteLine("============================");
        }

        private void PrintCurrencies()
        {
        }
    }
}
