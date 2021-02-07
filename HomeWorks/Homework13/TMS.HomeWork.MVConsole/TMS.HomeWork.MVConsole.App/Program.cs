using System;
using TMS.Homework.MVConsole.UI;
using TMS.HomeWork.MVConsole.App;

Console.WriteLine("Hello MVC!!!");

UI ui = new UI();
MyClass myClass = new MyClass();
//ui.View(myClass);

//ui.Edit(myClass);
var myClass1 = ui.Edit<MyClass>();
Console.WriteLine("Вернулись==========");
Console.WriteLine($"myClass.MyProperty1  {myClass1.MyProperty1}");
Console.WriteLine($"myClass.MyProperty2 {myClass1.MyProperty2}");



