using System;
using System.Collections.Generic;
using System.Threading;
using TMS.Homework.MVConsole.Service;

var record = new List<Foo>
{
    new Foo {ID = 1, Name = "One", Sex = "Male"},
    new Foo {ID = 2, Name = "Two", Sex = "Female"}
};
Console.WriteLine("Hello MVC!!!");
var csvService = new CsvService();

csvService.SaveToCSV(record);
csvService.SaveToCSV(record, csvService.GetSolutionRootFolder().FullName);
csvService.SaveToCSV(record, csvService.GetSolutionRootFolder().FullName, "MyFileName");
try
{
    csvService.SaveToCSV(record, csvService.GetSolutionRootFolder().FullName, "MyFileName");
}
catch (Exception e)
{
    Console.WriteLine(e);
}


Console.ReadLine();

