using System;

namespace TMS.ShopSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            var peopleGenerator = new PeopleGenerator();
            var shop = new ShopWithTreads(peopleGenerator, 3);
            //var shop = new ShopWithTasks(peopleGenerator, 3);
            //var shop = new ShopWithThreadPool(peopleGenerator, 3);
            Console.WriteLine($"Implementation: {shop.GetType().Name}");
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
                        break;
                }
            }
        }
    }
}
