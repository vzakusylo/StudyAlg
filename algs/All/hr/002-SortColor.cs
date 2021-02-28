using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SortColor002
{

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            int[] arr = new[] { 2, 0, 2, 1, 1, 0 };
            SortColors(arr);
            Console.WriteLine($"Output: {string.Join(", ", arr)}");
        }

        public static void SortColors(int[] arr)
        {
            int[] sorted = new int[3] { 0, 0, 0 };
            for (int i = 0; i < arr.Length; i++)
            {
                sorted[arr[i]] += 1;
            }
            int colorOneCount = sorted[0];
            int colorTwoCount = sorted[1];
            int colorThreeCount = sorted[2];

            for (int i = 0; i < colorOneCount; i++)
            {
                arr[i] = 0;
            }
            for (int i = colorOneCount; i < colorOneCount+colorTwoCount; i++)
            {
                arr[i] = 1;
            }
            for (int i = colorOneCount+colorTwoCount; i < colorOneCount+colorTwoCount+colorThreeCount; i++)
            {
                arr[i] = 2;
            }
        }
    }
}
