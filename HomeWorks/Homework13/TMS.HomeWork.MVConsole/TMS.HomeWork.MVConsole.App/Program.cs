using System;
using System.IO;
using TMS.Homework.MVConsole.Service;

Console.WriteLine("Hello MVC!!!");
var csvService = new CsvService();
//csvService.Persist<int>(null);
Console.WriteLine(csvService.GetSolutionRootFolder().FullName);
//Console.WriteLine(csvService.SaveToCSV());
Console.ReadLine();

