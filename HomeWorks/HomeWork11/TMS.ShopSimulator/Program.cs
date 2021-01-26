using System;
using System.Diagnostics;
using System.Threading;

namespace TMS.ShopSimulator
{
    class Program
    {
	    static int AvailableThreads
	    {
		    get
		    {
			    ThreadPool.GetAvailableThreads(out int availableThreads, out _);
			    return availableThreads;
		    }
	    }

	    static int MaxThreads
	    {
		    get
		    {
			    ThreadPool.GetMaxThreads(out int maxThreads, out _);
			    return maxThreads;
		    }
	    }

        static void Main(string[] args)
        {
            var peopleGenerator = new PeopleGenerator();
            //var shop = new ShopWithTreads(peopleGenerator, 300);
            //var shop = new ShopWithTasks(peopleGenerator, 3);
            //var shop = new ShopWithThreadPool(peopleGenerator, 3);
            var shop = new Shop(peopleGenerator, 3);
            Console.WriteLine($"Implementation: {shop.GetType().Name}");
            Console.WriteLine($"Max number of threads: {MaxThreads}");
            shop.Open();
            while (true)
            {
                var command = Console.ReadLine();
                switch (command)
                {
                    case "close":
                        shop.Close();
                        return;
                    default:
                        if (int.TryParse(command, out var numberOfPeople))
                        {
                            for (int i = 0; i < numberOfPeople; i++)
                            {
                                shop.EnterShop();
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Memory: {Process.GetCurrentProcess().PagedMemorySize64 / 1024} KB");
                            Console.WriteLine($"Available threads: {AvailableThreads}");
                        }
                        break;
                }
            }
        }
    }
}
