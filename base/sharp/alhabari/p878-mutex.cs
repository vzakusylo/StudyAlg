using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace page878
{
    [TestClass]
    public class Solution
    {
        // A Mutex is like a C# lock, but it can work across multiple processes. In other words,
        // Mutex can be computer-wide as well as application-wide.Acquiring and releasing an
        // uncontended Mutex takes around a microsecond—about 20 times slower than a lock.
        // With a Mutex class, you call the WaitOne method to lock and ReleaseMutex to
        // unlock.Just as with the lock statement, a Mutex can be released only from the same
        // thread that obtained it.

        [TestMethod]
        public void Run()
        {
            // unique to your company and application
            using (var mutex = new Mutex(true, "vados"))
            {
                // Wait a few seconds if contended, in case another instance 
                // of the program is still in the progress of shutting down.

                if (!mutex.WaitOne(TimeSpan.FromSeconds(3), false))
                {
                    Console.WriteLine("Another instance of the application is running. Bye!");
                    return;
                }
                try
                {
                    RunProgram();
                }
               
                finally
                {
                    mutex.ReleaseMutex();
                }

            }
        }

        private void RunProgram()
        {
            Console.WriteLine("Running. Press enter to exit.");            
        }
    }
}
