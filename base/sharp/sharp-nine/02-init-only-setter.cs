using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace sharp_nine_with_expression
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            WithExpressionBasicExample.Start();
        }
    }

    //Available in C# 9.0 and later, 
    // a with expression produces a copy of its record operand with 
    // the specified properties and fields modified:
    public class WithExpressionBasicExample
    {
        public record NamePoint(string Name, int X, int Y);

        public static void Start()
        {
            var p1 = new NamePoint("a", 0, 0);
            Console.WriteLine($"{nameof(p1)}: {p1}");

            var p2 = p1 with { Name = "B", X = 5 };
            Console.WriteLine($"{nameof(p2)}: {p2}");

            var p3 = p1 with
            {
                Name = "C",
                Y = 4
            };

            Console.WriteLine($"{nameof(p3)}: {p3}");
            Console.WriteLine($"{nameof(p1)}: {p1}");
        }
    }
}
