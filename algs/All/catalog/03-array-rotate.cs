using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArrayRotate
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            var rnd = new Random(1);
            for (var i = 0; i < 1000; i++)
            {
               
            }

            int[] a = { 1, 2, 3, 4, 5 };

            var b1 = new int[a.Length];
            Array.Copy(a, b1, a.Length);

            var b2 = new int[a.Length];
            Array.Copy(a, b2, a.Length);

            var b3 = new int[a.Length];
            Array.Copy(a, b3, a.Length);

            var b4 = new int[a.Length];
            Array.Copy(a, b4, a.Length);

            var b5 = new int[a.Length];
            Array.Copy(a, b5, a.Length);

            ArrayRotate.Rotate1(b1, 0, 1, a.Length);
            ArrayRotate.Rotate2(b2, 0, 1, a.Length);
            ArrayRotate.Rotate3(b3, 0, 1, a.Length);
            ArrayRotate.Rotate4(b4, 0,1, a.Length);
            ArrayRotate.Rotate5(b5, 0, 1, a.Length);
        }

    }

    public class ArrayRotate
    {
        public static void Rotate5(int[] arr, int first, int middle, int last)
        {
            Array.Reverse(arr, 0, 1);
        }

        public static void Rotate4<T>(T[] arr, int first, int shift, int last)
        {
            shift = shift % arr.Length;
            T[] buffer = new T[shift];
            Array.Copy(arr, buffer, shift);
            Array.Copy(arr, shift, arr, 0, arr.Length - shift);
            Array.Copy(buffer, 0, arr, arr.Length - shift, shift);
        }

        public static void LeftShiftArray<T>(T[] arr, int shift)
        {
           
        }

        public static void Rotate3(int[] a, int first, int middle, int last)
        {
            //int cur = 0;
            int n = last - first;
            int jump = middle - first;
            int gcd = Gcd(jump, n);
            int cycle = n / gcd;
            for (int i = 0; i < gcd; i++)
            {
                for (int j=0, cur = i; j < cycle - 1; j++)
                {
                    int next = cur + jump;
                    if (next >= n)
                    {
                        next -= n;
                    }
                    Swap(a,cur, next);
                    cur = next;
                }
            }
        }

        private static int Gcd(int a, int b)
        {
            //return a == 0 ? b : Gcd(b % a, a);

            while (true)
            {
                if (a == 0) return b;
                var a1 = a;
                a = b % a;
                b = a1;
            }
        }

        public static void Rotate1(int[] a, int first, int middle, int last)
        {
            int next = middle;
            while (first != next)
            {
                Swap(a, first++, next++);
                if (next == last)
                {
                    next = middle;  
                }else if (first == middle)
                {
                    middle = next;
                }
            }
        }

        public static void Rotate2(int[] a, int first, int middle, int last)
        {
            Reverse(a, first, middle);
            Reverse(a, middle, last);
            Reverse(a, first, last);
        }

        static void Reverse(int[] a, int from, int to)
        {
            while (from < --to)
            {
                Swap(a, from++, to);
            }
        }

        private static void Swap(int[] a, int i, int j)
        {
            //int t = a[j];
            //a[j] = a[i];
            //a[i] = t;

            // swap via deconstruction;
            (a[j], a[i]) = (a[i], a[j]);
        }
    }
}
