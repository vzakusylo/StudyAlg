using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace semaphore_slim_06
{
    // https://docs.microsoft.com/en-us/dotnet/api/system.threading.semaphoreslim?view=netframework-4.8

    // The following example creates a semaphore with a maximum count of three threads and an initial count 
    // of zero threads.The example starts five tasks, all of which block waiting for the semaphore. 
    // The main thread calls the Release(Int32) overload to increase the semaphore count to its maximum, 
    //which allows three tasks to enter the semaphore. Each time the semaphore is released, the previous 
    //semaphore count is displayed.Console messages track semaphore use.The simulated work interval is increased 
    // slightly for each thread to make the output easier to read.

    [TestClass]
    public class Solution
    {
        private static SemaphoreSlim semaphore;
        // A padding interval to make the output more orderly.
        private static int padding;


        [TestMethod]
        public void Main()
        {
            // Create the semaphore.
            semaphore = new SemaphoreSlim(0, 3);
            Console.WriteLine($"{semaphore.CurrentCount} tasks can enter the semaphore");
            Task[] tasks = new Task[5];

            //Create and start five numbered tasks.
            for (int i = 0; i <= 4; i++)
            {
                tasks[i] = Task.Run(async () =>
                {
                    // Each task begins by requesting the semaphore.
                    Console.WriteLine($"Task {Task.CurrentId} begins and wait for the semaphore");
                    int semaphoreCount;
                    await semaphore.WaitAsync(); // Blocks the current thread until it can enter the SemaphoreSlim.

                    try
                    {
                        Interlocked.Add(ref padding, 100);
                        Console.WriteLine($"Task {Task.CurrentId} enters the semaphore");
                        Thread.Sleep(1000 + padding);
                    }
                    finally
                    {
                        semaphoreCount = semaphore.Release(); //Releases the SemaphoreSlim object once.
                    }
                    Console.WriteLine($"Task {Task.CurrentId} releases the semaphore; previous count {semaphoreCount}");
                });
            }

            // Wait for half a second, to allow the tasks to start and block.
            Thread.Sleep(500);

            // Restore the semaphore count to its maximum value.
            Console.Write("Main thread calls Release(3) --> ");
            semaphore.Release(3); // Releases the SemaphoreSlim object a specified number of times.
            Console.WriteLine($"{semaphore.CurrentCount} tasks can enter the semaphore");

            // Main thread waits for the tasks to complete.
            Task.WaitAll(tasks);

            Console.WriteLine("Main thread exits");
        }
    }
}

// Calling WaitAsync on the semaphore produces a task that will be completed when that thread has been given 
// "access" to that token.await-ing that task lets the program continue execution when it is "allowed" to do so.
// Having an asynchronous version, rather than calling Wait, is important both to ensure that the method stays asynchronous,
// rather than being synchronous, as well as deals with the fact that an async method can be executing code across several threads, 
// due to the callbacks, and so the natural thread affinity with semaphores can be a problem.

//0 tasks can enter the semaphore
//Task 3 begins and wait for the semaphore
//Task 1 begins and wait for the semaphore
//Task 5 begins and wait for the semaphore
//Task 4 begins and wait for the semaphore
//Task 2 begins and wait for the semaphore
//Main thread calls Release(3) --> 3 tasks can enter the semaphore
//Task 1 enters the semaphore
//Task 5 enters the semaphore
//Task 2 enters the semaphore
//Task 1 releases the semaphore; previous count 0
//Task 4 enters the semaphore
//Task 5 releases the semaphore; previous count 0
//Task 3 enters the semaphore
//Task 2 releases the semaphore; previous count 0
//Task 4 releases the semaphore; previous count 1
//Task 3 releases the semaphore; previous count 2
//Main thread exits
