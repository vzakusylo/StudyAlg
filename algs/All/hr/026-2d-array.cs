using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _2d_Array
{
    // https://www.hackerrank.com/challenges/2d-array/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=interview-preparation-kit&playlist_slugs%5B%5D=arrays

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var arr = new List<List<int>>
            {
                new() {1, 1, 1, 0, 0, 0},
                new() {0, 1, 0, 0, 0, 0},
                new() {1, 1, 1, 0, 0, 0},
                new() {0, 0, 2, 4, 4, 0},
                new() {0, 0, 0, 2, 0, 0},
                new() {0, 0, 1, 2, 4, 0},
            };

            var res = hourglassSum1(arr);
            Assert.AreEqual(19, res);
        }

        public static int hourglassSum1(List<List<int>> arr)
        {
            var rows = arr.Count - 2;
            var columns = arr[0].Count - 2;
            var maxHourglassSum = -63;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    int currentHurglassSum = arr[i][j] + arr[i][j + 1] + arr[i][j + 2] +
                                             arr[i + 1][j + 1] +
                                             arr[i + 2][j] + arr[i + 2][j + 1] + arr[i + 2][j + 2];
                    maxHourglassSum = Math.Max(maxHourglassSum, currentHurglassSum);
                }
            }

            return maxHourglassSum;
        }

        public static int HourglassSum(int [][] arr)
        {
           // Contract.Requires(arr is not null);
            var rows = arr.Length - 2;
            var columns = arr[0].Length - 2;
            var maxHourglassSum = -63;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    int currentHurglassSum = arr[i][j]   +  arr[i][j+1] + arr[i][j+2] + 
                                                           arr[i+1][j+1] + 
                                             arr[i+2][j] + arr[i+2][j+1] + arr[i+2][j+2];
                    maxHourglassSum = Math.Max(maxHourglassSum, currentHurglassSum);
                }
            }

            return maxHourglassSum;
        }

    }
}
