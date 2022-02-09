using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace maxarraysum
{
    // https://www.hackerrank.com/challenges/max-array-sum/problem?isFullScreen=true

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var arr = new[] {3, 7, 4, 6, 5};
            var res = maxSubsetSum(arr);

            Assert.AreEqual(13, res);
        }

        static int maxSubsetSum(int[] arr)
        {
            if (arr.Length == 1) return arr[0];

            int[] result = new int[arr.Length];
            result[0] = arr[0];
            result[1] = arr[1] > arr[0] ? arr[1] : arr[0];

            for (int i = 2; i < arr.Length; i++)
            {
                var prevRes = Math.Max(result[i - 1], result[i - 2]);
                var curRes = Math.Max(arr[i], prevRes);
                result[i] = Math.Max(result[i - 2] + arr[i], curRes);
            }

            // return result[result.Length - 1];
            return result[^1];
        }
    }
}
