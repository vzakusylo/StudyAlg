using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TwoWaySignaling
{
    [TestClass]
    public class Solution
    {
        static EventWaitHandle first = new AutoResetEvent(false);
        static EventWaitHandle second = new AutoResetEvent(false);
        static object vadosLock = new object();
        static string value = string.Empty;

        [TestMethod]
        public async Task Run()
        {
            Task.Factory.StartNew(WorkedThread);
            Console.WriteLine("Main thread is waiting");    
            first.WaitOne(); // this line is waiting calling method 

            lock (vadosLock)
            {
                value = "Updating value in main thread";
                Console.WriteLine(value);
            }
            Thread.Sleep(1000);
            second.Set();
            Console.WriteLine("Released worker thread");
            Thread.Sleep(2000);
        }

        private void WorkedThread()
        {
            Thread.Sleep(1000);
            lock (vadosLock)
            {
                value = "Updating value in worker thread";
                Console.WriteLine(value);
            }
            first.Set(); // release main thread
            Console.WriteLine("Release main thread");
            Console.WriteLine("Worker thread is waiting");
            second.WaitOne();
        }
    }
}
