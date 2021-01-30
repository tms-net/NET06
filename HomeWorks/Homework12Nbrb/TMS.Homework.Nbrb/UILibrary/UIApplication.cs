using APILibrary;
using System;
using System.Collections.Generic;

namespace UILibrary
{
    public class UIApplication
    {
        private List<ShortCurrencies> currencyList = new List<ShortCurrencies>();
        private List<CurrencyCourse> currencyCourses = new List<CurrencyCourse>();
        public void ToDo()
        {
            Console.WriteLine("Hello! You are greeting by NbRb.");
            APIClient aPIClient = new APIClient();

            while (true)
            {
                PrintHelp();

                var commandLine = Console.ReadLine();

                Command command = new Command(commandLine);


                switch (command.Name)
                {
                    case "list":
                        int currencyNumber;
                        int.TryParse(command.Param, out currencyNumber);
                        if (currencyNumber > 0)
                        {
                            Console.WriteLine($"You selected {command.Param} currencies");
                            Console.WriteLine("======================================");
                            //currencyList = aPIClient.GetInfo(command.Param); // api группа делает привязку  на public void GetInfo ()
                        }
                        else
                        {
                            Console.WriteLine($"You selected all currencies");
                            Console.WriteLine("======================================");
                            //currencyList = aPIClient.GetInfo(); // api группа делает привязку  на public void GetInfo ()
                        }

                        PrintCurrencies();

                        break;

                    case "course":
                        bool isCurrencyExist = false;
                        if (currencyList.Count > 0)
                        {
                            int code;

                            bool res = int.TryParse(Console.ReadLine(), out code);
                            if (FindCurrencyInList(code) != null)
                                isCurrencyExist = true;
                            else
                            {
                                //if (aPIClient.FindCurrency(code) != null)
                                isCurrencyExist = true;
                            }

                        }

                        if (!isCurrencyExist)
                        {   
                            //if (aPIClient.FindCurrency(code) != null)
                            isCurrencyExist = true;
                        }

                        if (isCurrencyExist)
                        {
                            InputDates(out DateTime date1, out DateTime date2);

                            //List<CurrencyCourse> currencyCourses = aPIClient.GetCourses(code, date1, date2);
                            PrintCurrencyCourses(currencyCourses);
                        }
                      
                        break;
                    case "save":
                        {
                            Console.WriteLine("Input path:");
                            string path = Console.ReadLine();
                            if (currencyCourses.Count > 0)
                            { 
                             //FileService.SaveInFile(path, currencyCourses); }
                            }
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

        private ShortCurrencies FindCurrencyInList(int code)
        {
            foreach (var currency in currencyList)
            {
                if (currency.Code == code) return currency;
            }

            return null;
        }

        private void InputDates(out DateTime date1, out DateTime date2)
        {
            //bool firstDate = false;
            //bool secondDate = false;
            //date1 = new DateTime();
            //date2 = new DateTime();

            //while (!firstDate)
            //{
            //    Console.WriteLine("Enter start date from (day-month-year): ");
            //    firstDate = DateTime.TryParse(Console.ReadLine(), out date1);

            //    if (!firstDate)
            //    {
            //        Console.WriteLine("Error!!! Wrong format ( dd-mm-yyyy)!!! Please Try again ");
            //    }
            //}

            //while (!secondDate)
            //{
            //    Console.WriteLine("Enter the latest date (day-month-year): ");
            //    secondDate = DateTime.TryParse(Console.ReadLine(), out date2);

            //    if (!secondDate)
            //    {
            //        Console.WriteLine("Error!!! Wrong format ( dd-mm-yyyy)!!! Please Try again ");
            //    }


            //}
            date1 = InputDate("start");
            date2 = InputDate("end");
        }

        private DateTime InputDate(string nameOfDate)
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

        private void PrintCurrencyCourses(List<CurrencyCourse> currencyCourses)
        {
        }

        private void PrintHelp()
        {
            Console.WriteLine("Here must be HELP");
        }

        private void PrintCurrencies()
        {
        }
    }
}
