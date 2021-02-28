using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _07_QuickSort
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            int[] array = { 2, 8, 7, 1, 3, 5, 6, 4 };

            var p = new Solution();
            p.QuickSort(array, 0, 7);
        }

        
        public void QuickSort(int[] a, int p, int r)
        {
            if (p < r)
            {
                var q = Partition(a, p, r);
                QuickSort(a, p, q - 1);
                QuickSort(a, q + 1, r);
            }
        }

        private int Partition(int[] a, int p, int r)
        {
            var x = a[r];
            var i = p - 1;
            for (var j = p; j < r; j++)
                if (a[j] <= x)
                {
                    i = i + 1;
                    swap(ref a[i], ref a[j]);
                }

            swap(ref a[i + 1], ref a[r]);

            return i + 1;
        }

        private void swap(ref int x, ref int y)
        {
            var temp = x;
            x = y;
            y = temp;
        }
    }
}
