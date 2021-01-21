using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace TMS.ShopSimulator
{
    internal class Shop
    {
        private PeopleGenerator peopleGenerator;
        private ConcurrentQueue<Person> processingQueue;
        private List<Thread> processors;
        private bool isOpen;

        public Shop(PeopleGenerator peopleGenerator, int cashierNumber)
        {
            this.peopleGenerator = peopleGenerator;
            this.processingQueue = new ConcurrentQueue<Person>();

            var processors = new List<Thread>();
            for (int i = 0; i < cashierNumber; i++)
            {
                processors.Add(new Thread(ProcessPeople));
            }
            this.processors = processors;
        }

        internal void Open()
        {
            Console.WriteLine("Shop is opening...");
            isOpen = true;
            for (int i=1; i<=processors.Count; i++)
            {
                processors[i-1].Start(i);
                Console.WriteLine($"Cachier {i} is opened.");
            }
        }

        internal void Close()
        {
            isOpen = false;
        }

        internal void EnterShop()
        {
            processingQueue.Enqueue(peopleGenerator.GetPerson());
        }

        private void ProcessPeople(object obj)
        {
            while (isOpen)
            {
                while (!this.processingQueue.IsEmpty)
                {
                    if (processingQueue.TryDequeue(out var person))
                    {
                        Console.WriteLine($"Cachier {obj} is processing {person.Name}...");
                        Thread.Sleep(person.TimeToProcess);
                        Console.WriteLine($"Cachier {obj} is processed {person.Name}.");
                    }
                }
            }
            Console.WriteLine($"Cachier {obj} is closed.");
        }
    }
}