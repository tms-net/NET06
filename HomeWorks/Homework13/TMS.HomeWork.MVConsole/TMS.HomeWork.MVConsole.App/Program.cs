using System;
using System.Collections.Generic;
using System.Threading;
using TMS.Homework.MVConsole.Service;
using TMS.Homework.MVConsole.UI;
using TMS.HomeWork.MVConsole.App;

namespace TMS.HomeWork.MVConsole.App
{
    class Program
    {
        public static void Main(string[] args)
        {
            UI ui = new UI();

            Controller controller = new Controller(ui);
        }
    }
}