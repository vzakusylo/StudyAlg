using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace alternatingcharacters
{
    //https://www.hackerrank.com/challenges/alternating-characters

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var s = "AAAA";
            var res = alternatingCharacters(s);
            Assert.AreEqual(3, res);
        }

        public static int alternatingCharacters(string s)
        {
            int counter = 0;
            for (int i = 0; i < s.Length - 1; i++)
            {
                if (s[i] == s[i + 1])
                {
                    counter++;
                }
            }
            return counter;
        }

    }
}
