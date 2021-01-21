using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace TMS.NET06.Lesson11.Multithreading
{
    static class Program
    {
        public static void Main()
        {            
            var sw = new Stopwatch();
            var dates = Enumerable.Repeat("invalid", 10000).Concat(new [] {"2021-01-01"}).ToArray();
            sw.Start();
            foreach (var d in dates)
            {
                //if (DateTime.TryParse(d, out DateTime date))
                if (TryParse(d, out DateTime date))
                    Console.WriteLine(date);
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
        }

        private static bool TryParse(string value, out DateTime date)
        {
            date = DateTime.Now;
            try
            {
                date = DateTime.Parse(value);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
