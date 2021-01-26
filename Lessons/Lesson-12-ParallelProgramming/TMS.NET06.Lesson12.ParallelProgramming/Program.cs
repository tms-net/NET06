using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TMS.NET06.Lesson12.ParallelProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            // Calculate prime numbers using a simple (unoptimized) algorithm.
            
            IEnumerable<int> numbers = Enumerable.Range(3, 100000 - 3);

            var parallelQuery = from n in numbers.AsParallel()
                                where Enumerable.Range (2, (int) Math.Sqrt (n)).All(i => n % i > 0)
                                select n;            
            
            int[] primes = parallelQuery.ToArray();

            foreach(var prime in primes) {
                Console.WriteLine(prime);
            }
        }
    }
}
