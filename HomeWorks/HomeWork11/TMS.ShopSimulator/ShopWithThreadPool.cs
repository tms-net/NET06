using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TMS.ShopSimulator
{
	internal class ShopWithThreadPool
    {
        private readonly PeopleGenerator peopleGenerator;
        private readonly Queue<Person> peopleQueue;
        private readonly Queue<Cashier> cashierQueue;
        private readonly ManualResetEventSlim shopSynchronizator;
        private readonly SemaphoreSlim peopleSynchronizator;
        private readonly List<Cashier> allCashiers;

        private bool isOpen;

        public ShopWithThreadPool(PeopleGenerator peopleGenerator, int cashierNumber)
        {
	        // the factory to create Person instances
            this.peopleGenerator = peopleGenerator;

            // the queue to hold people to process
            this.peopleQueue = new Queue<Person>();

            // the queue to hole currently opened cashiers
            this.cashierQueue = new Queue<Cashier>();

            // the synchronization primitive to make possible to complete ongoing people processing
            this.shopSynchronizator = new ManualResetEventSlim(false);

            // the synchronization primitive to avoid cyclical waiting for people to arrive (alternative to while() {Thread.Sleep()})
            this.peopleSynchronizator = new SemaphoreSlim(0);

            // all cashiers that exist in the shop
            this.allCashiers = Enumerable.Range(1, cashierNumber).Select(n => new Cashier(n)).ToList();
        }

        internal void Open()
        {
            Console.WriteLine("Shop is opening...");
            isOpen = true;
            foreach (var cashier in allCashiers)
            {
	            // start processing routine on ThreadPool threads instead of creating new threads
                // this is an example that thread management can be delegated to a separate component
                // though it is not optimal to make these threads never quit the routine
                ThreadPool.QueueUserWorkItem(ProcessPeople);

                // make the cashier available for processing
	            EnqueueCashier(cashier);
	            Console.WriteLine($"Cashier {cashier.Name} is opened.");
            }
        }

        internal void Close()
        {
            isOpen = false;
            // when shop is closing all the routines should quit
            peopleSynchronizator.Release();

            // by requirements all people should be processed before quit
            shopSynchronizator.Wait();
        }

        internal void EnterShop()
        {
	        if (isOpen)
	        {
				EnqueuePerson(peopleGenerator.GetPerson());
	        }
        }

        private void EnqueuePerson(Person person)
        {
            // locking here because Queue<> is not thread safe, so mutating it state in a multi-thread environment can
            // lead to some unexpected behavior (for example when queue expansion occur internally)
            lock (peopleQueue)
            {
                peopleQueue.Enqueue(person);
            }

            // signalling to all processing threads that queue was changed
            peopleSynchronizator.Release();
        }

        private void EnqueueCashier(Cashier cashier)
        {
	        // locking here because Queue<> is not thread safe, so mutating it state in a multi-thread environment can
	        // lead to some unexpected behavior (for example when queue expansion occur internally)
            lock (cashierQueue)
            {
                cashierQueue.Enqueue(cashier);
            }
            // we should not signal here because the number of threads processing routine is equal to number of opened cashiers
            // it means that there won't be any thread that will end to forever cycle 
        }

        private bool TryDequeuePerson(out Person person)
        {
            person = null;
            lock (peopleQueue)
            {
                if (peopleQueue.Count > 0)
                {
                    person = peopleQueue.Dequeue();
                    return true;
                }
            }
            return false;
        }

        private bool TryDequeueCashier(out Cashier cashier)
        {
            cashier = null;
            lock (cashierQueue)
            {
                if (cashierQueue.Count > 0)
                {
                    cashier = cashierQueue.Dequeue();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// The routine for queue processing.
        /// </summary>
        /// <param name="obj">the state for processing as of <see cref="WaitCallback"/>s</param>
        private void ProcessPeople(object obj)
        {
	        // should be in a cycle in order to continuously process people by cashiers without stopping the thread
            // the Cashier class instances were added on order to describe properties of cashiers and they are processed
            // by cyclically removing and adding to the queue after processing of person by a certain cashier
	        // pros:
	        //    - thread management is delegated to the ThreadPool
            //    - we will get as many threads as we need for opened cashiers
	        // cons:
	        //    - it is not yet optimal to use these threads for such kind of processing
	        //    - the routine is a forever cycle, but threads are meant to be always available for other work
	        //    - we cannot get the result of the processing operation easily
            //    - we need additional synchronization for wait for all required work to complete
            while (isOpen)
            {
                while (TryDequeueCashier(out var cashier))
                {
                    while (TryDequeuePerson(out var person))
                    {
                        var timeToProcess = person.TimeToProcess + cashier.TimeToProcess;
                        Console.WriteLine($"Cashier {cashier.Name} is processing {person.Name} for {timeToProcess} ms on thread: {Thread.CurrentThread.ManagedThreadId}...");
                        Thread.Sleep(timeToProcess);
                    }
                    // cashier should not be enabled when the shop is closed and there are no people to process
                    // in order to quit the cashier dequeuing cycle
                    if (isOpen)
                    {
                        Console.WriteLine($"Cashier {cashier.Name} processed all people and waiting on thread: {Thread.CurrentThread.ManagedThreadId}...");
                        
                        // make the cashier enable for processing again
                        EnqueueCashier(cashier);

                        // waiting for people to arrive
                        this.peopleSynchronizator.Wait();
                    }
                }
            }
            // the first thread that is quitting is signaling that there are no more people to process and shop can be closed
            shopSynchronizator.Set();
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is quitting.");
        }
    }
}