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
            Console.WriteLine(string.Join(", ", GetShiftedArray(new int[] { 1, 2, 3, 4, 5 }, 3))); // 3, 4, 5, 1, 2
            Console.WriteLine(string.Join(", ", GetShiftedArray(new int[] { 1, 2, 3, 4, 5 }, 5))); // 1, 2, 3, 4, 5
            Console.WriteLine(string.Join(", ", GetShiftedArray(new int[] { -5, -11, 0, 9, 3}, 2))); // 9, 3, -5, -11, 0
        }

        static int [] GetShiftedArray(int[] arr, int kshift)
        {
            int length = arr.Length;            
            int k = kshift %= length;
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
