using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace page894
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Run()
        {
            object locker1 = new object();
            object locker2 = new object();

            new Thread(() =>
            {
                lock (locker1)
                {
                    Thread.Sleep(10000);
                    lock (locker2) ; // Deadlock
                }
            }).Start();

            lock (locker2)
            {
                Thread.Sleep(10000);
                lock (locker1) ; // Deadlock
            }
        }
    }
}
