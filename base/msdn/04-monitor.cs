using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

// https://docs.microsoft.com/en-us/dotnet/api/system.threading.monitor?view=netframework-4.8
namespace monitor_04
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            List<Task> tasks = new List<Task>();
            Random rnd = new Random();
            int total = 0;
            int n = 0;

            for (int taskCtr = 0; taskCtr < 10; taskCtr++)
            {
                tasks.Add(Task.Run(() => { 
                    int[] values = new int[10000];
                    int taskTotal = 0;
                    int taskN = 0;
                    int ctr = 0;
                    Monitor.Enter(rnd);
                    for (ctr = 0; ctr < 10000; ctr++)
                    {
                        values[ctr] = rnd.Next(0, 1001);
                    }
                    Monitor.Exit(rnd);

                    taskN = ctr;
                    foreach (var value in values)
                    {
                        taskTotal += value;
                    }
                    Console.WriteLine("Mean for task {0,2}: {1:N2} (N={2:N0})", Task.CurrentId, (taskTotal * 1.0) / taskN, taskN);
                    Interlocked.Add(ref n, taskN);
                    Interlocked.Add(ref total, taskTotal);
                }));
            }

            try
            {
                Task.WhenAll(tasks.ToArray());
            }
            catch(AggregateException e)
            {
                foreach (var ie in e.InnerExceptions)
                {
                    Console.WriteLine("{0}: {1}", ie.GetType().Name, ie.Message);
                }
            }
        }
    }
}
