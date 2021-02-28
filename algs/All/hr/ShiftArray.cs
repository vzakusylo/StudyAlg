using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShiftArray
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            Console.WriteLine(string.Join(", ", GetShiftedArrayUsingAdditionalArray(new int[] { 1, 2, 3, 4, 5 }, 3)));

            Console.WriteLine(string.Join(", ", GetShiftedArray(new int[] { 1, 2, 3, 4, 5, 6, 7 }, 4))); // 4, 5, 6, 7, 1, 2, 3
            Console.WriteLine(string.Join(", ", GetShiftedArray(new int[] { 1, 2, 3, 4, 5 }, 3))); // 3, 4, 5, 1, 2
            Console.WriteLine(string.Join(", ", GetShiftedArray(new int[] { 1, 2, 3, 4, 5 }, 5))); // 1, 2, 3, 4, 5
            Console.WriteLine(string.Join(", ", GetShiftedArray(new int[] { -5, -11, 0, 9, 3}, 2))); // 9, 3, -5, -11, 0
        }

        static int [] GetShiftedArrayUsingAdditionalArray(int[] arr, int k)
        {
            var length = arr.Length;
            var newArray = new int[length];
            for (int i = 0; i < length; i++)
            {
                var newIndex = (i + k) % length;
                newArray[newIndex] = arr[i];
            }
            return newArray;
        }

        static int [] GetShiftedArray(int[] arr, int k)
        {
            int length = arr.Length;    // 7        
            k %= length;    //4
            if (k == 0)
            {
                return arr;
            }
            int count = 0;
            for (int i = 0; count < length; i++)
            {
                int curIndex = i;
                int prev = arr[i];
                do
                {
                    int nextIndex = (curIndex + k) % length;
                    int temp = arr[nextIndex];
                    arr[nextIndex] = prev;
                    curIndex = nextIndex;
                    prev = temp;
                    count++;

                } while (curIndex != i);
            }
            return arr;
        }
    }
}
