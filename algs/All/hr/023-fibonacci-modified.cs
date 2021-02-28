using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace fibonacci_modified
{
    //https://www.hackerrank.com/challenges/fibonacci-modified/problem?utm_campaign=challenge-recommendation&utm_medium=email&utm_source=7-day-campaign

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {

            var res = fibonacciModifiedIterative(0, 1, 5);
            Assert.AreEqual(5uL, res); 

            res = fibonacciModifiedIterative(0, 1, 6);
            Assert.AreEqual(27uL, res);
          
            res = fibonacciModifiedIterative(0, 1, 10);
            Assert.AreEqual(1886167576011600224uL, res);

            res = fibonacciModifiedRecursive(0, 1, 5);
            Assert.AreEqual(5uL, res);

            res = fibonacciModifiedRecursive(0, 1, 6);
            Assert.AreEqual(27uL, res);
        }

        static ulong fibonacciModifiedIterative(int t1, int t2, int n)
        {
            ulong a = (ulong)t1;
            ulong b = (ulong)t2;
            ulong c = 0;
            for (int i = 2; i < n; i++)
            {
                c = a + b*b;
                a = b;
                b = c;
            }
            return c;
        }

        static ulong fibonacciModifiedRecursive(ulong t1, ulong t2, ulong n)
        {           
            return fibonacciModifiedRecursiveInternal(t1, t2, 1, n);
        }

        static ulong fibonacciModifiedRecursiveInternal(ulong a, ulong b, ulong counter, ulong len)
        {
            if (counter < len)
            {
                return fibonacciModifiedRecursiveInternal(b, a + b*b, counter + 1, len);
            }
            return a;
        }
    }
}
