using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace TMS.NET06.Lesson11.Multithreading
{
    static class Program
    {
        // A semaphore is like a nightclub: it has a certain capacity, enforced by a bouncer.
        // A semaphore with a capacity of one is similar to a Mutex or lock,
        // except that the semaphore has no “owner” — it’s thread-agnostic.
        // Any thread can call Release on a Semaphore, whereas with Mutex and lock,
        // only the thread that obtained the lock can release it.

        static SemaphoreSlim _sem = new SemaphoreSlim(3);    // Capacity of 3

        static void Main()
        {
            for (int i = 1; i <= 5; i++) new Thread(Enter).Start(i);
        }

        static void Enter(object id)
        {
            Console.WriteLine(id + " wants to enter");
            _sem.Wait();
            Console.WriteLine(id + " is in!");           // Only three threads
            Thread.Sleep(1000 * (int)id);               // can be here at
            Console.WriteLine(id + " is leaving");     // a time.
            _sem.Release();
        }
    }
}
