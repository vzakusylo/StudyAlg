using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace twostrings
{
    // https://www.hackerrank.com/challenges/two-strings

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var s1 = "wouldyoulikefries";
            var s2 = "abcabcabcabcabcabc";

            var res = twoStrings(s1, s2);

            Assert.AreEqual("NO", res);

            s1 = "hackerrankcommunity";
            s2 = "cdecdecdecde";

            res = twoStrings(s1, s2);

            Assert.AreEqual("YES", res);
        }

        public static String twoStrings(String s1, String s2)
        {
            HashSet<char> string1Chars = new HashSet<char>();
            HashSet<char> string2Chars = new HashSet<char>();

            for (int i = 0; i < s1.Length; i++)
            {
                string1Chars.Add(s1[i]);
            }

            for (int i = 0; i < s2.Length; i++)
            {
                string2Chars.Add(s2[i]);
            }

            var res = string1Chars.Intersect(string2Chars);

            if (res.Count() != 0)
            {
                return "YES";
            }
            else
            {
                return "NO";
            }
        }
    }
}
