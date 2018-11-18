using System;

namespace LonleyInteger
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[] { 2, 5, 5, 9, 19, 8, 7 };
            var res = LonleyInterger(array);
            Console.WriteLine(res);
        }

        public static int LonleyInterger(int[] array)
        {
            int result = 0;
            foreach (var item in array)
            {
                result ^= item;
            }
            return result;
        }
    }
}
