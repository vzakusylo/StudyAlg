using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace sync_context_07
{
    // https://habr.com/en/post/482354/?fbclid=IwAR1nF_kSz0V6xo8Bu1HF839sIUJfoJbxLy5kwsvls55fh_00BmK5d07RpTc

    [TestClass]
    public class Solution
    {
       
        [TestMethod]
        public async Task Main()
        {

            await Example1();
            await Example2();
        }

        public async Task Example1()
        {
            var cesp = new ConcurrentExclusiveSchedulerPair();
            Task.Factory.StartNew(() =>
            {
                Console.WriteLine(TaskScheduler.Current == cesp.ExclusiveScheduler);
            }, default, TaskCreationOptions.None, cesp.ExclusiveScheduler).Wait();
        }

        public async Task Example2()
        {
            Task t;
            SynchronizationContext old = SynchronizationContext.Current;
            SynchronizationContext.SetSynchronizationContext(null);

            try
            {
                t = CallCodeThatUsesAwaitAsync();
            }
            finally
            {
                SynchronizationContext.SetSynchronizationContext(old);
            }
        }

        public static async Task CallCodeThatUsesAwaitAsync()
        {
            Task t = Task.Run(() => { });
            await t;
            var res = Task.Delay(100);
             
        }
    }
}
