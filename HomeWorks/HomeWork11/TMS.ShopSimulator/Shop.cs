using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TMS.ShopSimulator
{
    internal class Shop
    {
        private readonly PeopleGenerator peopleGenerator;
        private readonly List<Cashier> allCashiers;
        private readonly List<Task<Cashier>> cashierTasks;
        private readonly Dictionary<Cashier, Task<Cashier>> cashierTasksDict;
        private readonly SemaphoreSlim cashierSynchronizator;

        private bool isOpen;
		private Task lastTask;

		public Shop(PeopleGenerator peopleGenerator, int cashierNumber)
        {
            this.peopleGenerator = peopleGenerator;
            this.allCashiers = Enumerable.Range(1, cashierNumber).Select(n => new Cashier(n)).ToList();
            this.cashierTasks = new List<Task<Cashier>>();
            this.cashierTasksDict = new Dictionary<Cashier, Task<Cashier>>();
            this.cashierSynchronizator = new SemaphoreSlim(1);
            this.lastTask = Task.CompletedTask;
        }

        internal void Open()
        {
            Console.WriteLine("Shop is opening...");
            isOpen = true;
            foreach (var cashier in allCashiers)
            {
                cashierTasks.Add(Task.FromResult(cashier));
                cashierTasksDict.TryAdd(cashier, Task.FromResult(cashier));
                Console.WriteLine($"Cashier {cashier.Name} is opened.");
            }
        }

        internal void Close()
        {
            isOpen = false;
            lastTask.Wait();
        }

        internal void EnterShop()
        {
            if (isOpen)
            {
	            var task = Task.Factory.StartNew(
		            () => ProcessPerson(peopleGenerator.GetPerson()),
		            TaskCreationOptions.LongRunning);
	            lastTask = Task.WhenAll(lastTask, task);
            }
        }

        private void ProcessPerson(Person person)
        {
	        var tcs = new TaskCompletionSource<Cashier>();

	        Cashier freeCashier;
            lock (cashierTasks)
            {
		        var completedInd = Task.WaitAny(cashierTasks.ToArray());
		        freeCashier = cashierTasks[completedInd].Result;
		        cashierTasks[completedInd] = tcs.Task;
            }

            var timeToProcess = freeCashier.TimeToProcess + person.TimeToProcess;
	        Console.WriteLine($"Cashier {freeCashier.Name} is processing {person.Name} on thread {Thread.CurrentThread.ManagedThreadId}...");
	        Thread.Sleep(timeToProcess);
	        Console.WriteLine($"PROCESSED: Cashier {freeCashier.Name} of {person.Name} on thread {Thread.CurrentThread.ManagedThreadId}...");

	        tcs.SetResult(freeCashier);
        }

        private async Task ProcessPersonAsync(Person person)
        {
            var tcs = new TaskCompletionSource<Cashier>();

            await cashierSynchronizator.WaitAsync();
            Cashier freeCashier;
            {
	            freeCashier = (await Task.WhenAny(cashierTasksDict.Values)).Result;
	            cashierTasksDict[freeCashier] = tcs.Task;
            }
            cashierSynchronizator.Release();

            var timeToProcess = freeCashier.TimeToProcess + person.TimeToProcess;
	        Console.WriteLine($"Cashier {freeCashier.Name} is processing {person.Name} on thread {Thread.CurrentThread.ManagedThreadId}...");
	        Thread.Sleep(timeToProcess);
	        Console.WriteLine($"PROCESSED: Cashier {freeCashier.Name} of {person.Name} on thread {Thread.CurrentThread.ManagedThreadId}...");
            
	        tcs.SetResult(freeCashier);
        }
    }
}