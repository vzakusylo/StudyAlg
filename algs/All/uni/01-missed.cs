using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace uni_missed
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            var set = new[] {4,3,2,7,8,2,3,1 };
            var actual = GetMissedNumbers(set);
            var expected = new[] { 5, 6 };

            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        public List<int> GetMissedNumbers(int[] arr)
        {
            var result = new List<int>();

            var min = arr.Min();
            var max = arr.Max();

            var hits = Enumerable.Range(min, max).ToDictionary(x => x, y => false);
            for (int i = 0; i < arr.Length; i++)
            {
                hits[arr[i]] = true;                
            }

            return hits.Where(x => x.Value == false).Select(x => x.Key).ToList();
        }
    }
}
