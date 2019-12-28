using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreadSafety
{
    [TestClass]
    public class Solution
    {
        static Dictionary<int, string> items = new Dictionary<int, string>();

        [TestMethod]
        public async Task Run()
        {
           var task1 = Task.Factory.StartNew(AddItem);
           var task2 = Task.Factory.StartNew(AddItem);
           var task3 = Task.Factory.StartNew(AddItem);
           var task4 = Task.Factory.StartNew(AddItem);
           var task5 = Task.Factory.StartNew(AddItem);
           
            Task.WaitAll(task3, task2, task1, task4, task5);
        }

        private void AddItem()
        {
            lock (items)
            {
                Console.WriteLine($"Lock acquired by {Task.CurrentId}");
                items.Add(items.Count, $"vados top man {items.Count}");
            }
            Dictionary<int, string> dictionary;
            lock (items)
            {
                Console.WriteLine($"Lock 2 acqured by {Task.CurrentId}");
                dictionary = items;
                foreach (var kvp in dictionary)
                {
                    Console.WriteLine($"k:{kvp.Key} v:{kvp.Value}");
                }
            }
            
            
            // ----------- thread unsafety
            //if (items.ContainsKey(1))
            //{

            //}
            //else
            //{
            //    items.Add(1, "Hello world");
            //}
        }
    }
}
