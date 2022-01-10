using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace template1
{
    // https://www.hackerrank.com/challenges/minimum-swaps-2

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var arr = new[] {1, 3, 5, 2, 4, 6, 7};
            var res = MinimumSwaps(arr);
            Assert.AreEqual(3, res);
            res = MinimumSwaps2(arr);
            Assert.AreEqual(3, res);

            arr = new[] {2, 3, 4, 1, 5};
            res = MinimumSwaps(arr);
            Assert.AreEqual(3, res);
            res = MinimumSwaps2(arr);
            Assert.AreEqual(3, res);

            arr = new[] { 3, 7, 6, 9, 1, 8, 10, 4, 2, 5 };
            res = MinimumSwaps(arr);
            Assert.AreEqual(9, res);
            res = MinimumSwaps2(arr);
            Assert.AreEqual(3, res);

            arr = new[] { 4,3,1,2 };
            res = MinimumSwaps(arr);
            Assert.AreEqual(3, res);
            res = MinimumSwaps2(arr);
            Assert.AreEqual(3, res);
        }

        static int MinimumSwaps(int[] arr)
        {
            var count = 0;
            var i = 0;
            while (i < arr.Length)
            {
                var index = arr[i] - 1;
                if (arr[i] != arr[index])
                {
                    (arr[i], arr[index]) = (arr[index], arr[i]);
                    count+=1;
                }
                else
                {
                    i+=1;
                }
            }
            
            return count;

        }

        static int MinimumSwaps2(int[] arr)
        {
            var numSwaps = 0;
            for (var index = 0; index < arr.Length; ++index)
            {
                while (index + 1 != arr[index])
                {
                    var swapIndex = arr[index] - 1;
                    int valAtIndex = arr[index];
                    int valAtSwapIndex = arr[swapIndex];
                    arr[index] = valAtSwapIndex;
                    arr[swapIndex] = valAtIndex;
                    ++numSwaps;
                }
            }
            return numSwaps;

        }
    }
}
