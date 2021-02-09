using APILibrary.Models;
using FileLibrary;
using System;
using UILibrary;

//namespace Nbrb
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            var uIClient = new UIApplication();

//            uIClient.ToDo();
//        }
//    }
//}

//var uIClient = new UIApplication(new FileService<ShortRate>);
var uIClient = new UIApplication();
await uIClient.ToDo();