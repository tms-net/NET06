using APILibrary;
using APILibrary.Models;
using FileLibrary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FileLibrary;
using System.Linq;

namespace UILibrary
{
    public class UIApplication
    {
        private List<ShortCurrency> currencyList;
        private List<ShortRate> currencyShortRates;
        //public UIApplication(IFileService fileService)
        //{

        //}

        public async Task ToDo()
        {
            Console.WriteLine("Hello! You are greeted by NbRb.");
            APIClient aPIClient = new APIClient();

            PrintHelp();

            while (true)
            {
                Console.WriteLine("Enter a command:");
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
                        Console.WriteLine("============================");
                        // currencyList = aPIClient.GetShortCurrencies(currencyNumber);

                        currencyList = (await aPIClient.GetShortCurrenciesAsync());
                        if (currencyNumber > 0) currencyList = currencyList.Take(currencyNumber).ToList();


                        PrintCurrencies();

                        break;

                    case "exrate":
                        ShortCurrency selectedCurrensy = null;
                        Console.WriteLine("Enter currency code:");
                        int.TryParse(Console.ReadLine(), out int code);
                        currencyShortRates = null;
                        bool isCurrExists = false;
                        if (currencyList != null)
                        {
                            if (currencyList.Count > 0)
                            {
                                selectedCurrensy = FindCurrencyInList(code);
                                if (selectedCurrensy != null)
                                    isCurrExists = true;
                            }
                        }

                        //search is not implemented in api.. 
                        //if (selectedCurrensy == null)
                        //{
                        //    //selectedCurrensy = aPIClient.FindCurrency(code);
                        //}


                        //if (selectedCurrensy != null)
                        //{

                        if (command.Param == "p")
                        {
                           bool isInputBreak = InputDates(out DateTime date1, out DateTime date2);
                            if (!isInputBreak) break;
                            currencyShortRates = await aPIClient.GetRatesAsync(date1, date2, code);

                        }
                        else
                        {
                            DateTime date = InputDate(out bool isInputBreak, "a");
                            if (isInputBreak) break;
                            // Rate rate = aPIClient.GetRates(date, code);
                            Rate rate = await aPIClient.GetRatesAsync(date, code);
                            if (rate != null)
                            {
                                currencyShortRates = new List<ShortRate>();
                                ShortRate shortRate = new ShortRate();
                                shortRate.Date = rate.Date;
                                shortRate.Cur_OfficialRate = (decimal)rate.Cur_OfficialRate;
                                currencyShortRates.Add(shortRate);
                            }
                        }
                        PrintCurrencyRates(code, currencyShortRates, isCurrExists);
                        // }

                        break;
                    case "save":
                        {
                            Console.WriteLine("Input path:");
                            string path = Console.ReadLine();
                            if (currencyShortRates != null)
                            {
                                if (currencyShortRates.Count > 0)
                                {
                                    //FileService.SaveInFile(path, currencyShortRates); }
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
            if (currencyList == null)
            {
                Console.WriteLine("Somethings went wrong. Currency list is null");
                return null;
            }

            foreach (var currency in currencyList)
            {
                if (currency.Code == code) return currency;
            }

            return null;
        }

        private bool InputDates(out DateTime date1, out DateTime date2)
        {
            bool res = true;
            do
            {
                date1 = InputDate(out bool isInputBreak, "start");
                if (!isInputBreak)
                {
                    date2 = InputDate(out isInputBreak, "end");
                    if (isInputBreak)  return false; 
                }
                else { date2 = new DateTime(); return false; }

                if (date1 > date2) { res = false; Console.WriteLine("Error! First date shouldn't be greater than second date."); }
            } while (!res);

            return true;
        }

        private DateTime InputDate(out bool isInputBreak, string nameOfDate = "")
        {
            bool isOk = false;
            isInputBreak = false;
            DateTime date = new DateTime();

            while (!isOk)
            {
                Console.WriteLine($"Enter {nameOfDate} date (day-month-year): ");
                
                ConsoleKeyInfo firstKey = Console.ReadKey();
                if (firstKey.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine($"\n Do you want to break date input? If Yes Press Y/y else press any key:");
                    var key = Console.ReadKey().Key;
                    if (key == ConsoleKey.Y) 
                    {
                        isInputBreak = true; 
                        Console.WriteLine();
                        return date;
                    }

                }
                string userInput = firstKey.KeyChar + Console.ReadLine();
                isOk = DateTime.TryParse(userInput, out date);

                if (!isOk)
                {
                    Console.WriteLine("Error!!! Wrong format ( dd-mm-yyyy)!!! Please Try again ");
                }
            }

            return date;
        }

        private void PrintCurrencyRates(int currCode, List<ShortRate> currencyShortRates, bool isCurrExists)
        {
            if (currencyShortRates == null)
            {
                if (isCurrExists)
                    Console.WriteLine("Somethings went wrong.");
                else
                    Console.WriteLine($"Somethings went wrong. May be currency code {currCode} isn't exist.");
                return;
            }
            Console.WriteLine("*****Currency rates*****");
            Console.WriteLine($"Currency code: {currCode}");
            foreach (var shortRate in currencyShortRates)
            {
                Console.WriteLine($"Date: {shortRate.Date.ToShortDateString()}, rate: {shortRate.Cur_OfficialRate}");
            }
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
            if (currencyList == null)
            {
                Console.WriteLine("Somethings went wrong. Currency list is null");
                return;
            }

            Console.WriteLine("*****List of currencies*****");
            Console.WriteLine("\nCODE   ABBR    NAME");

            foreach (var shortCurrency in currencyList)
            {
                Console.WriteLine("{0:d3}    {1}     {2}", shortCurrency.Code, shortCurrency.Abbreviation, shortCurrency.Name);
            }
        }
    }
}
