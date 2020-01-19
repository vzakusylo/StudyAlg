using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace blocking_collections
{   
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public async Task Main()
        {
            await AddTakeDemo.BC_AddTakeCompleteAdding();
            TryTakeDemo.BC_TryTake();
            FromToAnyDemo.BC_FromToAny();
        }
    }
    class FromToAnyDemo
    {
        public static void BC_FromToAny()
        {
            BlockingCollection<int>[] bcs = new BlockingCollection<int>[2];
            bcs[0] = new BlockingCollection<int>(5); // collection bounded to 5 items
            bcs[1] = new BlockingCollection<int>(5);

            // Should be able to add 10 items w/o blocking
            int numFailtures = 0;
            for (int i = 0; i < 10; i++)
            {
                if (BlockingCollection<int>.TryAddToAny(bcs,i) == -1)
                {
                    numFailtures++;
                }
            }
            Console.WriteLine("TryAddToAny: {0} failures (should be 0)", numFailtures);

            int numItems = 0;
            int item;
            while (BlockingCollection<int>.TryTakeFromAny(bcs, out item) != -1) numItems++;
            Console.WriteLine("TryTakeFromAny:retrieved {0} items (should be 10)", numItems);
        }
    }

    class AddTakeDemo
    {
        // Demonstrate:
        //    BlockingCollection<T>.Add()
        //    BlockingCollection<T>.Take()
        //    BlockingCollection<T>.CompleteAdding()

        public static async Task BC_AddTakeCompleteAdding()
        {
            using (BlockingCollection<int> bc = new BlockingCollection<int>())
            {
                // spin up a Task to populate the blocking collection.
                using (Task t1 = Task.Run(() => 
                {
                    bc.Add(1);
                    bc.Add(2);
                    bc.Add(3);
                    bc.CompleteAdding();
                }))
                {
                    // spin up a task to consume the blocking collection
                    using (Task t2 = Task.Run(() =>
                    {
                        try
                        {
                            while (true)
                            {
                                Console.WriteLine(bc.Take());
                            }
                        }
                        catch (InvalidOperationException)
                        {
                            Console.WriteLine("That's all");
                        }
                    }))
                    {
                        await Task.WhenAll(t1, t2);
                    }
                }               
            }
        }
    }

    class TryTakeDemo
    {
        public static void BC_TryTake()
        {
            using (BlockingCollection<int> bc = new BlockingCollection<int>())
            {
                int numItems = 10000;
                for (int i = 0; i < numItems; i++)
                {
                    bc.Add(i);
                }
                bc.CompleteAdding();
                int outerSum = 0;

                // Delegate for consuming the blocking collection and adding up all items
                Action action = () =>
                {
                    int localItem;
                    int localSum = 0;

                    while (bc.TryTake(out localItem))
                    {
                        localSum += localItem;
                    }
                    Interlocked.Add(ref outerSum, localSum);
                };

                // Launch three parallel actions to consume the BlockingCollection
                Parallel.Invoke(action, action, action);

                Console.WriteLine("Sum[0..{0}) = {1}, should be {2}", numItems, outerSum, ((numItems * (numItems -1)) / 2));
                Console.WriteLine("bc.IsCompleted = {0} (should be true)", bc.IsCompleted);
            }
        }
    }
}