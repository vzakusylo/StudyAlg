using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DiagonalDifference
{
    // https://www.hackerrank.com/challenges/diagonal-difference/problem

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
           
        }

        public static int DiagonalDifference(List<List<int>> arr)
        {
            int leftToRight = 0;
            int rightToLeft = 0;

            int rows = arr.Count;
            int columns = arr[0].Count;

            int i = 0;
            int j = 0;
            int k = 0;
            int l = arr.Count - 1;

            while (i < rows && j < columns && k < rows && l >= 0)
            {
                leftToRight += arr[i][j];
                rightToLeft += arr[k][l];
                i += 1;
                j += 1;
                k += 1;
                l -= 1;
            }
            return Math.Abs(leftToRight - rightToLeft);

        }
    }
}
