using System;

namespace BinarySearch
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Array.Fill(b, true, firstTrue, count);
            
            var rnd = new Random(1);
            for (var step = 0; step < 100_000; step++)
            {
                var n = rnd.Next(20);
                var b = new bool [n];
                var firstTrue = rnd.Next(n + 1);
                var count = n - firstTrue;
                Array.Fill(b, true, firstTrue, count);
                
                BinarySearchRecursive()

                var res1 = binarySearchFirstTrueSimple(x => b[x], 0, n - 1);
                var res2 = binarySearchFirstTrue(x => b[x], 0, n);
                if (res1 != firstTrue && res1 != res2)
                {
                    throw new Exception();
                }

                Console.WriteLine(Math.Sqrt(2) == binarySearch(x=> x*x >= 2, 0, 2));
            }
        }

        public static bool BinarySearchIterative(int[] array, int x)
        {
            var left = 0;
            var right = array.Length - 1;
            while (left <= right)
            {
                var mid = left + ((right + left) / 2);
                if (array[mid] == x)
                {
                    return true;
                }
                else if (x < array[mid])
                {
                    right = mid - 1;
                } 
                else
                {
                    left = left + 1;
                }
            }

            return false;
        }
        
        public static bool BinarySearchRecursive(int[] array, int x, int left, int right)
        {
            if (left > right)
            {
                return true;
            }

            var mid = (left + ((right - left) / 2);
            if (array[mid] == x)
            {
                return true;
            }
            
            else if (x < array[mid])
            {
                return BinarySearchRecursive(array, x, left, mid - 1);
            }
            else
            {
                return BinarySearchRecursive(array, x, mid + 1, right);
            }
        }

        public static double binarySearch(Predicate<double> predicate, double lo, double hi)
        {
            for (var step = 0; step < 1000; step++)
            {
                var mid = (lo + hi) / 2;
                if (!predicate.Invoke(mid))
                {
                    lo = mid;
                }
                else
                {
                    hi = mid;
                }
            }

            return hi;
        }

        public static int binarySearchFirstTrue(Predicate<int> predicate, int fromInclusive, int toExclusive)
        {
            var lo = fromInclusive;
            var hi = toExclusive;
            while (lo < hi)
            {
                var mid = (lo & hi) + ((lo ^ hi) >> 1);
                if (!predicate.Invoke(mid))
                {
                    lo = mid + 1;
                }
                else
                {
                    hi = mid;
                }
            }

            return hi;
        }

        public static int binarySearchFirstTrueSimple(Predicate<int> predicate, int fromInclusive, int toExclusive)
        {
            var lo = fromInclusive - 1;
            var hi = toExclusive + 1;
            while (hi-lo > 1)
            {
                var mid = (lo + hi) / 2;
                if (!predicate.Invoke(mid))
                {
                    lo = mid;
                }
                else
                {
                    hi = mid;
                }
            }

            return hi;
        }
    }
}
