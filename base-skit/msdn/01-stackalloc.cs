using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace stackallocDemo
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            int length = 3;
            Span<int> numbers = stackalloc int[length];
            for (int i = 0; i < length; i++)
            {
                numbers[i] = i;
            }
            // https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/operators/stackalloc
        }
    }
}
