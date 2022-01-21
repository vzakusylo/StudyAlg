using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace maxmin
{
    // https://www.hackerrank.com/challenges/one-month-preparation-kit-angry-children/problem?h_r=internal-search&isFullScreen=true

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            // test case https://hr-testcases-us-east-1.s3.amazonaws.com/226691/input07.txt?AWSAccessKeyId=AKIAR6O7GJNX5DNFO3PV&Expires=1642720827&Signature=MGxS8nfEMz5sgkFqwfwX%2BukYZdY%3D&response-content-type=text%2Fplain

        }

        public static int maxMin(int k, List<int> arr)
        {
            arr.Sort();

            int minValue = int.MaxValue;

            for (int i = 0; i < arr.Count() - k; i++)
            {
                int currentMin = int.MaxValue;
                int currentMax = int.MinValue;
                for (int j = i; j < i + k; j++)
                {
                    currentMin = Math.Min(currentMin, arr[j]);
                    currentMax = Math.Max(currentMax, arr[j]);
                }

                minValue = Math.Min(minValue, currentMax - currentMin);
            }

            return minValue;
        }

    }
}


