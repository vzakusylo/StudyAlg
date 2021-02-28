using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace pangram
{
    // https://www.hackerrank.com/challenges/hackerrank-in-a-string

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var result = pangrams("We promptly judged antique ivory buckles for the next prize");
            Assert.AreEqual("pangram", result);

            result = pangrams("We promptly judged antique ivory buckles for the prize");
            Assert.AreEqual("not pangram", result);
        }

        static string pangrams(string s)
        {
            var hrs = "abcdefghjklmnopqrstuvwxyz";
            var allChars = new bool[hrs.Length];

            for (int i = 0; i < s.Length; i++)
            {
                var sc = s[i];
                for (int j = 0; j < hrs.Length; j++)
                {
                    if (char.ToLower(s[i]) == char.ToLower(hrs[j]))
                    {
                        allChars[j] = true;
                        break;
                    }
                }
            }

            return allChars.All(x => x == true) ? "pangram" : "not pangram";
        }
    }
}
