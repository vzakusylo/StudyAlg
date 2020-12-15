using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace correctness_invariant
{
    //https://www.hackerrank.com/challenges/correctness-invariant/problem?utm_campaign=challenge-recommendation&utm_medium=email&utm_source=24-hour-campaign

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            insertionSort(new[]  { 7, 4, 3, 5, 6, 2 });
        }

        public static void insertionSort(int[] A)
        {
            var j = 0;
            for (var i = 0; i < A.Length; i++)
            {
                var value = A[i];
                j = i - 1;
                while (j > 0 && value < A[j])
                {
                    A[j + 1] = A[j];
                    j = j - 1;
                }
                A[j + 1] = value;
            }
            Console.WriteLine(string.Join(" ", A));
        }


    }
}
