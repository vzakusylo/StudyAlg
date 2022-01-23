using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace stringconstruction
{
    // https://www.hackerrank.com/challenges/string-construction/problem?h_r=internal-search&isFullScreen=true

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var res = stringConstruction("abcd");
            Assert.AreEqual(4, res);


        }

        public static int stringConstruction(string s)
        {
            // abab
            var hashSet = new HashSet<char>();
            int cost = 0;
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (!hashSet.Contains(c))
                {
                    cost++;

                    hashSet.Add(c);
                }
            }
            return cost;
        }


    }
}
