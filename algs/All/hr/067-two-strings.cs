using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace twostrings2
{
    // https://www.hackerrank.com/challenges/two-strings/problem?h_r=internal-search&isFullScreen=true

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var res = twoStrings("hello", "world");
            Assert.AreEqual("YES", res);
        }

        public static string twoStrings(string s1, string s2)
        {
            // solution 1

            int[] arr1 = new int[26];
            int[] arr2 = new int[26];

            for (int i = 0; i < s1.Length; i++)
            {
                arr1[s1[i] - 'a']++;
            }

            for (int i = 0; i < s2.Length; i++)
            {
                arr2[s2[i] - 'a']++;
            }

            for (int i = 0; i < 26; i++)
            {
                if (arr1[i] != 0 && arr2[i] != 0)
                {
                    return "YES";
                }
            }

            return "NO";
        }
    }
}
