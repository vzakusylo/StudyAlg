using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace p4_parralel
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
           
        }

        void DoActions(Action action, CancellationToken token)
        {
            Action[] actions = Enumerable.Repeat(action, 20).ToArray();
            Parallel.Invoke(new ParallelOptions(){CancellationToken = token}, actions);
        }

        public void Process(double[] arr)
        {
            Parallel.Invoke(
                () => ProcessParallel(arr, 0, arr.Length/2),
                () => ProcessParallel(arr, arr.Length/2, arr.Length)
                );
        }

        private void ProcessParallel(double[] arr, int start, int end)
        {
            for (int j = start; j < end; j++)
            {
               var element = arr[j];
            }
        }
    }
}
