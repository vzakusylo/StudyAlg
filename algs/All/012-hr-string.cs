using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace hackerrank_in_a_string
{
    // https://www.hackerrank.com/challenges/hackerrank-in-a-string

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var result = hackerrankInString("hhaacckkekraraannk");
            Assert.AreEqual("YES", result);

            result = hackerrankInString("rhbaasdndfsdskgbfefdbrsdfhuyatrjtcrtyytktjjt");
            Assert.AreEqual("NO", result);
        }

        // Complete the hackerrankInString function below.
        static string hackerrankInString(string s)
        {
            var hrs = "hackerrank";
            var allChars = new bool[hrs.Length];
                                    
            var hrc = 0;
            var asc = 0;

            while (hrc < hrs.Length && asc < s.Length)
            {
                if (hrs[hrc] == s[asc])
                {
                    allChars[hrc++] = true;
                    asc++;
                }
                else
                {
                    asc++;
                }
            }

            return allChars.All(x => x == true) ? "YES" : "NO";
        }

    }
}
