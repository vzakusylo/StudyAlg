using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static MoreLinq.Extensions.AggregateExtension;

namespace more_linq_aggregate
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            var res = Enumerable.Range(1, 100).Aggregate((x, y) => { return x + y; });
            Console.WriteLine(res);
        }
    }
}
