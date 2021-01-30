using APILibrary;
using FileLibrary;
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
            APIClient aPIClient = new APIClient();
            //bool exit = false;
            while (true)
            {
                var commandLine = Console.ReadLine();

                //int.TryParse(command, out var numberOfPeople)
                Command command = new Command(commandLine);
              

                switch (command.Name)
                {
                    case "list":

                        Console.WriteLine("You selected 10 currency");
                       

                        //currencyList = aPIClient.GetInfo(command.Param); // api группа делает привязку  на public void GetInfo ()
                        break;

                    case "course":
                        if (currencyList.Count > 0)
                        {
                            int code;
                            bool isCurrencyExist = false;
                            bool res = int.TryParse(Console.ReadLine(), out code);
                            if (FindCurrencyInList(code) != null)
                                   isCurrencyExist = true;
                            
                            else
                            {
                                //if (aPIClient.FindCurrency(code) != null)
                                    isCurrencyExist = true; 
                            }

                            if (isCurrencyExist)
                            {
                                InputDates(out DateTime date, out DateTime date1);
                               
                               //List<CurrencyCourse> currencyCourses = aPIClient.GetCourses(code, date1, date2);
                                PrintCurrencyCourses(currencyCourses);
                            }
                        }
                        break;
                    case "save":
                        {
                            Console.WriteLine("Input path");
                            string path = Console.ReadLine();
                            if (currencyCourses.Count > 0)
                            { //FileService.SaveInFile(path, currencyCourses); }
                               
                            }
                            break;
                        }
                    case "exit":
                        // exit = true;
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

        //private static bool InputDate(out DateTime date, string NameOfDate)
        //{
        //    Console.WriteLine($"Please input {NameOfDate} date");
        //    bool result = DateTime.TryParse(Console.ReadLine(), out date);
        //    if (result != true) Console.WriteLine("Date is incorrect! Try again!");
        //    return result;

        //}

        //private bool InputDatePeriod(out DateTime start, out DateTime end)
        //{
        //    //while (InputDate(out start, nameof(start)) != true) ;
        //    //while (InputDate(out end, nameof(end)) != true) ;

        //    //bool result = (start <= end) ? true : false;
        //    //if (!result) Console.WriteLine("Error! Start date is greater than end date. Try again!");
        //    //return result;

        //    return false;

        //}

        private void InputDates(out DateTime date, out DateTime date1)
        {
            bool firstDate = false;
            bool secondDate = false;
            date = new DateTime();
            date1 = new DateTime();

            while (!firstDate)
            {
                Console.WriteLine("Enter start date from the range (day-month-year): ");
                firstDate = DateTime.TryParse(Console.ReadLine(), out date);

                if (!firstDate)
                {
                    Console.WriteLine("Error!!! Wrong format ( dd-mm-yyyy)!!! Please Try again ");
                }
            }

            while (!secondDate)
            {
                Console.WriteLine("Enter the latest date from the range (day-month-year): ");
                secondDate = DateTime.TryParse(Console.ReadLine(), out date1);

                if (!secondDate)
                {
                    Console.WriteLine("Error!!! Wrong format ( dd-mm-yyyy)!!! Please Try again ");
                }


            }


        }

        private void PrintCurrencyCourses(List<CurrencyCourse> currencyCourses)
        { 
        }

        private void PrintHelp()
        {
        }
    }
}
