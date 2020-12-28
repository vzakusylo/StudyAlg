using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace big_sorting
{
    //https://www.hackerrank.com/challenges/big-sorting

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var input = new List<string> {
                "8",
                "1",
                "2",
                "100",
                "12303479849857341718340192371",
                "3084193741082937",
                "3084193741082938",
                "111",
                "200" 
            };

            input.Sort(new CustomComparer());

            var output = new List<string>
            {
                "1",
                "2",
                "8",
                "100",
                "111",
                "200",
                "3084193741082937",
                "3084193741082938",
                "12303479849857341718340192371"                
            };

            CollectionAssert.AreEqual(output, input);
        }
    }

    internal class CustomComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            // If the length is not the same, we return the difference.
            // A negative # means, x Length is shorter, 0 means the same (this doesn't occur) and a postive # means Y is bigger
            if (x.Length != y.Length) return x.Length - y.Length;

            // Now the length is the same.
            // Compare the number from the first digit.
            for (int i = 0; i < x.Length; i++)
            {
                char left = x[i];
                char right = y[i];
                if (left != right)
                    return left - right;
            }

            // Default: "0" means both numbers are the same.
            return 0;
        }
    }
}
