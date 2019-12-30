using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CountDown
{
    [TestClass]
    public class Solution
    {
        static CountdownEvent countDown = new CountdownEvent(5);
        // signal when count reach 0
        // its going to wait worker thread signal to be called X amount of times

        [TestMethod]
        public async Task Run()
        {
            Task.Factory.StartNew(DoWork);
            Task.Factory.StartNew(DoWork);
            Task.Factory.StartNew(DoWork);
            Task.Factory.StartNew(DoWork);
            Task.Factory.StartNew(DoWork);
            countDown.Wait();
            Console.WriteLine("Signal has been called 5 times");
        }   
       

        private void DoWork()
        {
            Thread.Sleep(250);
            Console.WriteLine(Task.CurrentId + " is calling signal");
            countDown.Signal();
        }
    }
}
