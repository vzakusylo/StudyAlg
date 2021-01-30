using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ManualEventReset
{
    [TestClass]
    public class Solution
    {
      
        private static ManualResetEvent manualEvent = new ManualResetEvent(false);
        private static EventWaitHandle eventWaitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);

        [TestMethod]
        public async Task Run()
        {
            Task.Factory.StartNew(CallWaitOne);
            Task.Factory.StartNew(CallWaitOne);
            Task.Factory.StartNew(CallWaitOne);
            Task.Factory.StartNew(CallWaitOne);
            Task.Factory.StartNew(CallWaitOne);

            Thread.Sleep(2000);
            Console.WriteLine("All thread wiil be released");
            manualEvent.Set();
            Thread.Sleep(3000);

            Console.WriteLine("Press any key again. Threads won't block even if they call WaitOne");
            Task.Factory.StartNew(CallWaitOne);
            Task.Factory.StartNew(CallWaitOne);
            Task.Factory.StartNew(CallWaitOne);
            Task.Factory.StartNew(CallWaitOne);
            Task.Factory.StartNew(CallWaitOne);
            Thread.Sleep(3000);

            Console.WriteLine("Press any key again. Threads will block even if they call WaitOne");
            manualEvent.Reset();
            Task.Factory.StartNew(CallWaitOne);
            Task.Factory.StartNew(CallWaitOne);
            Task.Factory.StartNew(CallWaitOne);
            Task.Factory.StartNew(CallWaitOne);
            Task.Factory.StartNew(CallWaitOne);
            Thread.Sleep(3000);
        }   
       

        private void CallWaitOne()
        {
            Console.WriteLine(Task.CurrentId + " has called WaitOne");
            manualEvent.WaitOne();
            Console.WriteLine(Task.CurrentId + " finaly ended");
        }
    }
}
