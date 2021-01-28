using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TMS.ShopSimulator
{
	internal class ShopWithTreads
	{
		private PeopleGenerator peopleGenerator;
		private ConcurrentQueue<Person> peopleQueue;
		private List<Thread> processors;

		private bool isOpen;

		public ShopWithTreads(PeopleGenerator peopleGenerator, int cashierNumber)
		{
			// the factory to create Person instances
			this.peopleGenerator = peopleGenerator;

			// the queue to hold people to process
			this.peopleQueue = new ConcurrentQueue<Person>();

			// threads that are processing people (represent cashiers)
			this.processors = Enumerable.Range(0, cashierNumber).Select(n => new Thread(ProcessPeople)).ToList();
		}

		internal void Open()
		{
			Console.WriteLine("Shop is opening...");
			isOpen = true;
			for (int i = 1; i <= processors.Count; i++)
			{
				// start processing on all threads
				processors[i - 1].Start(i);
				Console.WriteLine($"Cashier {i} is opened.");
			}
		}

		internal void Close()
		{
			isOpen = false;
			// we should not wait for threads to complete because Thread is a Foreground thread by default
			// and process will wait until all threads are complete before exit, but to be correct we can do it:

			// foreach(var thread in processors) thread.Join();
		}

		internal void EnterShop()
		{
			if (isOpen)
			{
				// just add person to the end of the queue
				peopleQueue.Enqueue(peopleGenerator.GetPerson());
			}
		}

		/// <summary>
		/// The routine for queue processing.
		/// </summary>
		/// <param name="obj">thread index</param>
		private void ProcessPeople(object obj)
		{
			// should be in a cycle in order to continuously process people without stopping the thread
			// pros:
			//    - thread is always doing what it is meant to do, it is controlled by Shop class
			// cons:
			//    - in most cases threads are not doing any useful work, waiting for new people
			//    - it is complicated to easily open and close the 'cashier'
			//    - class should be extended to support any of real cashier properties
			while (isOpen)
			{
				// temporarily suspending thread should be considered here to avoid high CPU load
				// Thread.Sleep(100);
				while (!this.peopleQueue.IsEmpty)
				{
					if (peopleQueue.TryDequeue(out var person))
					{
						Console.WriteLine($"Cashier {obj} is processing {person.Name}...");
						Thread.Sleep(person.TimeToProcess);
						Console.WriteLine($"Cashier {obj} is processed {person.Name}.");
					}
				}
			}
			Console.WriteLine($"Cashier {obj} is closed.");
		}
    }
}