using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TMS.ShopSimulator
{
    internal class Shop
    {
        private PeopleGenerator peopleGenerator;
        private Queue<Cashier> cashierQueue;
        private List<Cashier> allCashiers;
        private List<Task> currentTasks;

        private bool isOpen;

        public Shop(PeopleGenerator peopleGenerator, int cashierNumber)
        {
            this.peopleGenerator = peopleGenerator;
            this.cashierQueue = new Queue<Cashier>();
            this.allCashiers = new List<Cashier>();
            this.currentTasks = new List<Task>();
            for (int i = 0; i < cashierNumber; i++)
            {
                allCashiers.Add(new Cashier());
            }
        }

        internal void Open()
        {
            Console.WriteLine("Shop is opening...");
            isOpen = true;
            for (int i=0; i < allCashiers.Count; i++)
            {
                EnqueueCashier(allCashiers[i]);
                Console.WriteLine($"Cachier {allCashiers[i].Name} is opened.");
            }
        }

        internal void Close()
        {
            isOpen = false;
            Task.WaitAll(currentTasks.ToArray());
        }

        internal void EnterShop()
        {
            if (isOpen)
            {
                // public delegate void WaitCallback(object state);
                var task = Task.Run(() => ProcessPerson(peopleGenerator.GetPerson()));
                currentTasks.Add(task);
                //task.ContinueWith(t => currentTasks.Remove(task));
            }
        }

        private void ProcessPerson(Person person)
        {
            if (isOpen)
            {
                Cashier cashier;
                while (!TryDequeueCashier(out cashier))
                {
                    Thread.Sleep(100);
                }

                var timeToProcess = cashier.TimeToProcess + person.TimeToProcess;
                Console.WriteLine($"Cashier {cashier.Name} is processing {person.Name} on thread {Thread.CurrentThread.ManagedThreadId}...");
                Thread.Sleep(timeToProcess);
                EnqueueCashier(cashier);
            }
            Console.WriteLine($"Cachier {Thread.CurrentThread.ManagedThreadId} is exitting.");
        }

        private void EnqueueCashier(Cashier cashier)
        {
            lock(cashierQueue)
            {
                cashierQueue.Enqueue(cashier);
            }
        }

        private bool TryDequeueCashier(out Cashier cashier)
        {
            cashier = null;
            lock (this.cashierQueue)
            {
                if (cashierQueue.Count > 0)
                {
                    cashier = this.cashierQueue.Dequeue();
                    return true;
                }
            }
            return false;
        }
    }
}