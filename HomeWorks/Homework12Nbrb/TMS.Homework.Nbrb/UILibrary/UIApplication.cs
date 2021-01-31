using APILibrary;
using APILibrary.Models;
using System;
using System.Collections.Generic;

namespace UILibrary
{
    public class UIApplication
    {
        private List<ShortCurrency> currencyList;
        private List<ShortRate> currencyShortRates;
        public void ToDo()
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
                        currencyList = aPIClient.GetShortCurrencies(currencyNumber);
                        PrintCurrencies();

                        break;

                    case "exrate":
                        //ShortCurrency selectedCurrensy = null;
                        Console.WriteLine("Enter currency code:");
                        int.TryParse(Console.ReadLine(), out int code);
                        currencyShortRates = null;
                        // здесь должен был быть поиск...в api не реализовано. 
                        //if (currencyList!= null)
                        //{ 
                        //    if (currencyList.Count > 0)
                        //    {                           
                        //        selectedCurrensy = FindCurrencyInList(code);
                        //    }
                        //}

                        //if (selectedCurrensy == null)
                        //{
                        //    //selectedCurrensy = aPIClient.FindCurrency(code);
                        //}


                        //if (selectedCurrensy != null)
                        //{

                        if (command.Param == "p")
                        {
                            InputDates(out DateTime date1, out DateTime date2);
                            currencyShortRates = aPIClient.GetRates(date1, date2, code).listShortRate;
                        }
                        else
                        {
                            //if (currencyShortRates != null)
                            //    currencyShortRates.Clear();
                            //else currencyShortRates = new List<ShortRate>();

                            DateTime date = InputDate("a");
                            Rate rate = aPIClient.GetRates(date, code);
                            if (rate != null)
                            {
                                currencyShortRates = new List<ShortRate>();
                                ShortRate shortRate = new ShortRate();
                                shortRate.Date = rate.Date;
                                shortRate.Cur_OfficialRate = (decimal)rate.Cur_OfficialRate;
                                currencyShortRates.Add(shortRate);
                            }
                        }
                        PrintCurrencyRates(code, currencyShortRates);
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
            foreach (var currency in currencyList)
            {
                if (currency.Code == code) return currency;
            }

            return null;
        }

        private void InputDates(out DateTime date1, out DateTime date2)
        {
            bool res = true;
            do
            {
              date1 = InputDate("start");
              date2 = InputDate("end");

                if (date1 > date2) { res = false; Console.WriteLine("Error! First date shouldn't be greater than second date."); }
            } while (!res);
            
        }

        private DateTime InputDate(string nameOfDate = "")
        {
            bool isOk = false;
            DateTime date = new DateTime();

            while (!isOk)
            {
                Console.WriteLine($"Enter {nameOfDate} date (day-month-year): ");
                isOk = DateTime.TryParse(Console.ReadLine(), out date);

                if (!isOk)
                {
                    Console.WriteLine("Error!!! Wrong format ( dd-mm-yyyy)!!! Please Try again ");
                }
            }

            return date;
        }

        private void PrintCurrencyRates(int currCode, List<ShortRate> currencyShortRates)
        {
            if (currencyShortRates == null)
            { Console.WriteLine($"Somethings went wrong. May be currency code {currCode} isn't exist.");
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
            Console.WriteLine("*****List of currencies*****");
            Console.WriteLine("\nCODE   ABBR    NAME");

            foreach (var shortCurrency in currencyList)
            {
                Console.WriteLine("{0:d3}    {1}     {2}", shortCurrency.Code, shortCurrency.Abbreviation,shortCurrency.Name);
            }
        }
    }
}
