using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

// https://leetcode.com/problems/longest-palindromic-substring

namespace longestpalindromicsubstring
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            var initialString = "dabcba";
            var palindromExpected = "abcba";
            var resultPalindrom = LongestPalindrome(initialString);

            Assert.AreEqual(palindromExpected, resultPalindrom);

            initialString = "a";
            palindromExpected = "a";
            resultPalindrom = LongestPalindrome(initialString);

            Assert.AreEqual(palindromExpected, resultPalindrom);

            initialString = "ac";
            palindromExpected = "a";
            resultPalindrom = LongestPalindrome(initialString);

            Assert.AreEqual(palindromExpected, resultPalindrom);
        }

        public static string LongestPalindrome(string s)
        {
            int maxPalinLength = 0;
            string longestPalindrome = null;
            int length = s.Length;

            if (length == 1) return s;

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
