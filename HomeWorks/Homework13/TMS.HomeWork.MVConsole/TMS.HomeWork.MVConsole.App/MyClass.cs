using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.HomeWork.MVConsole.App
{
    class MyClass : IInfoClass
    {
        public double MyField;

        public MyClass()
        {
           
        }

        public int MyProperty1 { get; set; }
        public string MyProperty2 { get; set; }

        public void MyMethod(int i)
        // internal void MyMethod(int i)
        { Console.WriteLine("Hello"); }

        public double Sum()
        {return 0;}
        public void Info() { }
        public void Set(double d1, double d2) { }
    }

    interface IInfoClass
    {
        double Sum();
        void Info();
        void Set(double d1, double d2);
    }
}
