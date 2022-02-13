using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

//https://drive.google.com/file/d/0B7EfQdvL5qf_Q2JCWmZJRHQwdFU/view

namespace alg02
{
    [TestClass]
    public class LongestPalindromicSubstringNativApproach
    {
        [TestMethod]
        public void Main()
        {
            var initialString = "dabcba";
            var palindromExpected = "abcba";
            var resultPalindrom = LongestPalindrome(initialString);

            Assert.AreEqual(palindromExpected, resultPalindrom);
        }

        public static string LongestPalindrome(string s)
        {
            int maxPalinLength = 0;
            string longestPalindrome = null;
            int length = s.Length;

            for (int i = 0; i < length; i++)
            {
                for (int j = i + 1; j < length; j++)
                {
                    int len = j - i;
                    string current = s.Substring(i, len+1);
                    if (IsPalindrome(current))
                    {
                        if (len > maxPalinLength)
                        {
                            longestPalindrome = current;
                            maxPalinLength = len;
                        }
                    }
                }
            }
            return longestPalindrome;
        }

        private static bool IsPalindrome(string s)
        {
            for (int i = 0; i < s.Length -1; i++)
            {
                if (s[i] != s[s.Length - 1 - i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
