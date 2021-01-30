using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace parallel_dictionary
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public async Task Run()
        {
            var ordersQueue = new Queue<string>();
            Task task1 = Task.Run(() => PlaceOrders(ordersQueue, "Xavier", 5));
            Task task2 = Task.Run(() => PlaceOrders(ordersQueue, "Ramdevi", 5));

            Task.WaitAll(task1, task2);

            foreach (var order in ordersQueue)
            {
                Console.WriteLine($"ORDER: {order}");
            }
        }

        private static void PlaceOrders(Queue<string> orders, string customerName, int nOrders)
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
