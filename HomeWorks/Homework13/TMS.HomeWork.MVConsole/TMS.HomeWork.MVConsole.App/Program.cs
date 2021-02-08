using System;
using System.Collections.Generic;
using System.Threading;
using TMS.Homework.MVConsole.Service;
using TMS.Homework.MVConsole.UI;
using TMS.HomeWork.MVConsole.App;

var myList = new List<Foo>
{
    new Foo {ID = 1, Name = "One", Sex = "Male"},
    new Foo {ID = 2, Name = "Two", Sex = "Female"}
};
Console.WriteLine("Hello MVC!!!");
var csvService = new CsvService();

UI ui = new UI();
MyClass myClass = new MyClass();
//ui.View(myClass);

//ui.Edit(myClass);
var myClass1 = ui.Edit<MyClass>();
Console.WriteLine("Вернулись==========");
Console.WriteLine($"myClass.MyProperty1  {myClass1.MyProperty1}");
Console.WriteLine($"myClass.MyProperty2 {myClass1.MyProperty2}");



