using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace sherlockandarray
{
    // https://www.hackerrank.com/challenges/sherlock-and-array

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            // 1, 2, 3, 3
            // totalSum: 1 + 2 + 3 + 3 = 9
            // i = 0, a[i] = 1, 1 == 9 - 0 , 1 != 9 , leftSum += 1 (1), totalSum(9) -= 1 = 8
            // i = 1, a[i] = 2, 2 == 8 - 1 , 2 != 8 , leftSum += 2 (3), totalSum(8) -= 2 = 6
            // i = 2, a[i] = 3, 3 == 6 - 3 , 3 == 3 -------found
            var arr = new List<int> {1, 2, 3, 3};
            var res = balancedSums(arr);
            Assert.AreEqual("YES", res);
        }

        public static string balancedSums(List<int> arr)
        {
            if (arr.Count == 1) return "YES";

            int totalSum = 0;
            for (int i = 0; i < arr.Count; i++)
            {
                totalSum += arr[i];
            }

            int leftSum = 0;
            for (int i = 0; i < arr.Count; i++)
            {
                if (leftSum == (totalSum - arr[i]))
                {
                    return "YES";
                }
                leftSum += arr[i];
                totalSum -= arr[i];
            }

            return "NO";

        }


    }
}
