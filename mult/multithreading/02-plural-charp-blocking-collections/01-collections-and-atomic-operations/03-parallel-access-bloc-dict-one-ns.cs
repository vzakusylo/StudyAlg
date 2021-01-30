using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace parallel_bloc_dictionary
{
    // https://app.pluralsight.com/course-player?clipId=8db51c31-4f89-481c-8199-88a0d1d0fbef
// first run
//ORDER: Xavier wants t-short 0
//ORDER: Ramdevi wants t-short 0
//ORDER: Ramdevi wants t-short 1
//ORDER: Xavier wants t-short 1
//ORDER: Xavier wants t-short 2
//ORDER: Ramdevi wants t-short 2
//ORDER: Xavier wants t-short 3
//ORDER: Ramdevi wants t-short 3
//ORDER: Xavier wants t-short 4
//ORDER: Ramdevi wants t-short 4
// second run
//ORDER: Xavier wants t-short 0
//ORDER: Ramdevi wants t-short 0
//ORDER: Xavier wants t-short 1
//ORDER: Xavier wants t-short 2
//ORDER: Ramdevi wants t-short 1
//ORDER: Xavier wants t-short 3
//ORDER: Ramdevi wants t-short 2
//ORDER: Xavier wants t-short 4
//ORDER: Ramdevi wants t-short 3
//ORDER: Ramdevi wants t-short 4

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
                Thread.Sleep(1);
                string orderName = $"{customerName} wants t-short {i}";
                orders.Enqueue(orderName);
            }
        }
    }
}
