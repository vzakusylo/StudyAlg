using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace parallel_bloc_dictionary_one_ns
{
    // https://app.pluralsight.com/course-player?clipId=8db51c31-4f89-481c-8199-88a0d1d0fbef
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public async Task Run()
        {
            var ordersQueue = new ConcurrentQueue<string>();
            Task task1 = Task.Run(() => PlaceOrders(ordersQueue, "Xavier", 5));
            Task task2 = Task.Run(() => PlaceOrders(ordersQueue, "Ramdevi", 5));

            Task.WaitAll(task1, task2);

            foreach (var order in ordersQueue)
            {
                Console.WriteLine($"ORDER: {order}");
            }
        }

        private static void PlaceOrders(ConcurrentQueue<string> orders, string customerName, int nOrders)
        {
            for (int i = 0; i < nOrders; i++)
            {
                Thread.Sleep(new TimeSpan(1));
                string orderName = $"{customerName} wants t-short {i}";
                orders.Enqueue(orderName);
            }
        }
    }
}
