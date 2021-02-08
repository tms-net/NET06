using System;
using System.Collections.Generic;
using System.Threading;
using TMS.Homework.MVConsole.Service;

var myList = new List<Foo>
{
    new Foo {ID = 1, Name = "One", Sex = "Male"},
    new Foo {ID = 2, Name = "Two", Sex = "Female"}
};
Console.WriteLine("Hello MVC!!!");
var csvService = new CsvService();

csvService.SaveToCSV(myList);
csvService.SaveToCSV(myList, csvService.GetSolutionRootFolder().FullName);
csvService.SaveToCSV(myList, csvService.GetSolutionRootFolder().FullName, "MyFileName");
try
{
    csvService.SaveToCSV(myList, csvService.GetSolutionRootFolder().FullName, "MyFileName");
}
catch (Exception e)
{
    Console.WriteLine(e);
}

Console.ReadLine();

public class Foo
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Sex { get; set; }
}
