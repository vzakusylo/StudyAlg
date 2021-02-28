using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace two_strings
{
    // https://www.hackerrank.com/challenges/two-strings/

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var result = twoStrings("hello", "world");
            Assert.AreEqual("YES", result);

            result = twoStrings("hi", "world");
            Assert.AreEqual("NO", result);
        }

        static string twoStrings(string s1, string s2)
        {
            return s1.ToArray().Distinct().Intersect(s2.ToArray().Distinct()).Any() ? "YES" : "NO";

        }
    }
}
