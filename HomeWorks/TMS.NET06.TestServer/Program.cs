using System;
using System.Threading;

namespace TMS.NET06.TestServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            while (true)
            {
                Thread.Sleep(200);
            }
        }
    }
}
