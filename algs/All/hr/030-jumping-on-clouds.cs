using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace jumpingOnClouds
{
    //https://www.hackerrank.com/challenges/jumping-on-the-clouds

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var c = new[] {0, 0, 1, 0, 0, 1, 0};
            var res = jumpingOnClouds(c.ToList());
            Assert.AreEqual(4, res);

            c = new[] { 0, 0, 0, 1, 0, 0};
            res = jumpingOnClouds(c.ToList());
            Assert.AreEqual(3, res);
        }

        public static int jumpingOnClouds(List<int> c)
        {
            Contract.Assume(c is not null);

            int numJumps = 0;
            int i = 0;
            while (i < c.Count - 1)
            {
                if (i + 2 == c.Count || c[i+2] == 1)
                {
                    i++;
                    numJumps++;
                }
                else
                {
                    i += 2;
                    numJumps++;
                }
            }

            return numJumps;
        }

    }
}
