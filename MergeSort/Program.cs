using System;

namespace MergeSort
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public static void mergeSort(int[] array)
        {

        }

        public static void mergeSort(int[] array, int[] temp, int leftStart, int rightEnd)
        {
            if (leftStart >= rightEnd)
            {
                return;
            }
                
            int middle = (leftStart + rightEnd) / 2;
            mergeSort(array, temp, leftStart, middle);
            mergeSort(array, temp, middle+1, rightEnd);
            mergeHalves(array, temp, leftStart, rightEnd);
        }

        private static void mergeHalves(int[] array, int[] temp, int leftStart, int rightEnd)
        {
            int leftEnd = (rightEnd + leftStart)/2;
            int rightStart = leftStart + 1;
            int size = rightEnd - leftStart + 1;

            int left = leftStart;
            int right = rightStart;
            int index = leftStart;

            while (left <= leftEnd && right <= rightEnd)
            {
                if (array[left] <= array[right])
                {
                    temp[index] = array[left];
                    temp[index] = array[left];
                    left++;
                }
                else
                {
                    temp[index] = array[right];
                    right++;
                }
                index++;
            }

            Array.Copy(array, temp, left);
        }
    }
}
