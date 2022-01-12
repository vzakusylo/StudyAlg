using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace minimumabsolutedifferenceinanarray
{
    // https://www.hackerrank.com/challenges/minimum-absolute-difference-in-an-array

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
           
        }

        public static int minimumAbsoluteDifference(List<int> arr)
        {
            int minAbsoluteDiff = int.MaxValue;
            arr.Sort();

            for (int i = 0; i < arr.Count - 1; i++)
            {
                int currentAbsoluteDifference = Math.Abs(arr[i] - arr[i + 1]);
                minAbsoluteDiff = Math.Min(currentAbsoluteDifference, minAbsoluteDiff);
            }

            return minAbsoluteDiff;
        }
    }
}
