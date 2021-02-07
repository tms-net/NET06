using System;

namespace TMS.HomeWork.MVConsole.App
{
    public class Cat
    {
        /// <summary>
        /// кличка кота
        /// </summary>
        public string Nickname { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// статический конструктор
        /// </summary>
        public Cat()
        { Name = "Cat"; }

        //public Cat()
        //{ Name = "Cat"; }

        public void Speak()
        {
            Console.WriteLine($"Hello, I am {Name} {Nickname}");
        }

        public void Eat(object meal)
        {
           Console.WriteLine($"I am  Cat {Nickname}, i'm eating {meal}");
        }

    }
}
