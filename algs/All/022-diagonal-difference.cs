using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace diagonal_difference
{
    //https://www.hackerrank.com/challenges/diagonal-difference/problem

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            List<List<int>> arr = new List<List<int>> { new List<int> { 1, 2, 3 }, new List<int> { 4, 5, 6 }, new List<int> { 9, 8, 9 } };
            var res = diagonalDifference(arr);
            Assert.AreEqual(2, res); // 
        }

        public static int diagonalDifference(List<List<int>> arr)
        {
            var leftToRigh = 0;
            for (int i = 0; i < arr.Count; i++)
            {
                for (int j = 0; j < arr[i].Count; j++)
                {
                    if (i == j)
                    {
                        leftToRigh += arr[i][j];
                    }
                }
            }

            var rightToLeft = 0;
            for (int i = 0; i < arr.Count; i++)
            {
                for (int j = arr[i].Count-1; j >= 0 ; j--) // 0
                {
                    if (i + j == arr.Count-1) // 
                    {
                        rightToLeft += arr[i][j];
                    }
                }
            }

            return Math.Abs(leftToRigh - rightToLeft);
        }

    }
}
