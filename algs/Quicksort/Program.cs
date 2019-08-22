using System;
using System.Linq;

namespace Quicksort
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = {9, 2, 6, 4, 3, 5, 1};
            foreach (var i in array)
            {
                Console.Write(i);
            }
            Console.Write(Environment.NewLine);
            Quicksort(array, 0, 6);
            foreach (var i in array)
            {
                Console.Write(i);
            }
        }

        public static void Quicksort(int[] array, int left, int right)
        {
            if (left >= right)
            {
                return;
            }

            int pivot = array[(left + right) / 2];
            int index = Partition(array, left, right, pivot);
            Quicksort(array, left, index-1);
            Quicksort(array, index, right);
        }

        public static int Partition(int[] array, int left, int right, int pivot)
        {
            while (left <= right)
            {
                while (array[left] < pivot)
                {
                    left++;
                }

                while (array[right] > pivot)
                {
                    right--;
                }

                if (left <= right)
                {
                    Swap(array, left, right);
                    left++;
                    right--;
                }
            }

            return left;
        }

        private static void Swap(int[] array, int left, int right)
        {
            int temp = array[left];
            array[left] = array[right];
            array[right] = temp;
        }
    }
}
