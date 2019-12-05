using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fib
{
    [TestClass]
    public class Run
    {
        [TestMethod]
        public void Main()
        {
            Console.WriteLine("Hello World!");

            var res = Fib(5);
            Console.WriteLine("Hello World!" + res);
        }

        public static int Fib(int n)
        {
            if (n <= 0)
            {
                return 0;
            }
            else if (n == 1)
            {
                return 1;
            }
            else
            {

                var res = Fib(n - 1) + Fib(n - 2);
                Console.WriteLine($"Fib({n - 1}) + Fib({n - 2}) = {res}");
                return res;
            }
        }
    }
}
