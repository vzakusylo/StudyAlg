using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace insertionsort
{
    //https://www.hackerrank.com/challenges/sherlock-and-anagrams

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            int[] arr = new int[] { 2, 4, 6, 8, 3 };
            //insertionSort1(0, arr);
            //Assert.IsTrue(Enumerable.SequenceEqual(new int[] { 2, 3, 4, 6, 8 }, arr));

            arr = new int[] { 1, 3, 5, 9, 13, 22, 27, 35, 46, 51, 55, 83, 87, 23 };
            insertionSort1(0, arr);
            Assert.IsTrue(Enumerable.SequenceEqual(new int[] {  1, 3, 5, 9, 13, 22, 23, 27, 35, 46, 51, 55, 83, 87 }, arr));

            //arr = new int[] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 1 };
            //insertionSort1(0, arr);
            //Assert.IsTrue(Enumerable.SequenceEqual(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, arr));
        }

        //2 4 6 8 3
        //2 4 6 8 8 
        //2 4 6 6 8 
        //2 4 4 6 8 
        //2 3 4 6 8 

        // Complete the insertionSort1 function below.
        static void insertionSort1(int n, int[] arr)
        {
            var last = arr[arr.Length - 1];
            arr[arr.Length - 1] = arr[arr.Length - 2];

            for (int i = arr.Length-1; i >= 0; i--)
            {
                if (arr[i] > last)
                {
                    if (i - 1 >= 0)
                    {
                        arr[i] = arr[i - 1];
                        if (arr[i] + 1 > last)
                        {
                            Console.WriteLine(string.Join(" ", arr));
                        }
                    }
                    else
                    {
                        arr[i] = last;
                    }
                }
                else
                {
                    arr[i+1] = last;
                    break;
                }
            }
            Console.WriteLine(string.Join(" ", arr));
        }
    }
}
