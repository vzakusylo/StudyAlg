using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutoRsetEvent
{
    [TestClass]
    public class Solution
    {
        static EventWaitHandle autoRestEvent = new EventWaitHandle(false, EventResetMode.AutoReset);
        static AutoResetEvent eventWaitHandle = new AutoResetEvent(false);

        [TestMethod]
        public async Task Run()
        {
            Task.Factory.StartNew(WorkedThread);
            Thread.Sleep(2500);
            eventWaitHandle.Set();
        }

        private void WorkedThread()
        {
            Console.WriteLine("Waiting to enter the gate");
            eventWaitHandle.WaitOne();
            //Logic
            Console.WriteLine("Gate entered");
        }
    }
}
