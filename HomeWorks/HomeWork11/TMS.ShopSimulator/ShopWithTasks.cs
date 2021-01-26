using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TMS.ShopSimulator
{
	internal class ShopWithTasks
	{
		private readonly PeopleGenerator peopleGenerator;
		private readonly ConcurrentQueue<Cashier> cashierQueue;
		private readonly List<Cashier> allCashiers;

		private Task lastTask;
		private bool isOpen;

		public ShopWithTasks(PeopleGenerator peopleGenerator, int cashierNumber)
		{
			// the factory to create Person instances
			this.peopleGenerator = peopleGenerator;

			// the queue to hole currently opened cashiers
			this.cashierQueue = new ConcurrentQueue<Cashier>();

			// the task to hold all required work to be done
			this.lastTask = Task.CompletedTask;

			// all cashiers that exist in the shop
			this.allCashiers = Enumerable.Range(1, cashierNumber).Select(n => new Cashier(n)).ToList();
		}

		internal void Open()
		{
			Console.WriteLine("Shop is opening...");
			isOpen = true;
			foreach (var cashier in allCashiers)
			{
				// just enable cashier for processing
				// we are not making any thread work here
				EnqueueCashier(cashier);
				Console.WriteLine($"Cashier {cashier.Name} is opened.");
			}
		}

		internal void Close()
		{
			isOpen = false;
			// waiting for the task that should wait for all required tasks to complete
			lastTask.Wait();
		}

		internal void EnterShop()
		{
			if (isOpen)
			{
				// scheduling processing work to ThreadPool by Task API
				var task = Task.Run(() => ProcessPerson(peopleGenerator.GetPerson()));
				// update the task to hold all required work by using Task API
				// TODO: determine how optimal it is
				lastTask = Task.WhenAll(lastTask, task);
			}
		}

		/// <summary>
		/// The delegate to process a single person.
		/// </summary>
		/// <param name="person">a person to process</param>
		private void ProcessPerson(Person person)
		{
			// in this example thread are not making any routine jon but the processing of a single person is scheduled instead
			// the only cyclic work to be done is to wait for free cashier, this has some pitfalls but can be resolved by Task API
			// pros:
			//    - thread management is delegated to the ThreadPool and can be extended by Task API
			//    - we will get as many threads as we need for processing the people
			//    - we will not occupy any thread when no work should be done, so they can perform other useful work
			//    - we can store the result of processing and handle it as we need with TaskAPI
			//    - we can use Task API to wait for all work for completion
			// cons:
			//    - there is still some cyclic work that can block the thread

			if (isOpen)
			{
				// we are making a hopefully short while cycle to get free cashier
				Cashier cashier;
				while (!TryDequeueCashier(out cashier))
				{
					Thread.Sleep(100);
					//await Task.Delay(100);
				}

				var timeToProcess = cashier.TimeToProcess + person.TimeToProcess;
				Console.WriteLine(
					$"Cashier {cashier.Name} is processing {person.Name} on thread {Thread.CurrentThread.ManagedThreadId}...");
				Thread.Sleep(timeToProcess);

				// make the cashier enable for processing again
				EnqueueCashier(cashier);
			}

			Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is exiting.");
		}

		private void EnqueueCashier(Cashier cashier)
		{
			// use a separate method to easily switch dequeuing implementation
			// without changing main processing method (for ex. we can use signalling)
			cashierQueue.Enqueue(cashier);
		}

		private bool TryDequeueCashier(out Cashier cashier)
		{
			// use a separate method to easily switch dequeuing implementation
			// without changing main processing method
			cashier = null;
			return !cashierQueue.IsEmpty && cashierQueue.TryDequeue(out cashier);
		}
	}
}