using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ctcimakinganagrams
{
    // https://www.hackerrank.com/challenges/ctci-making-anagrams

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
           
        }

        public static int makeAnagram(string a, string b)
        {
            int min_deletions = 0;
            int[] a_frequences = new int [26];
            int[] b_frequences = new int [26];

            for (int i = 0; i < a.Length; i++)
            {
                var curChar = a[i];
                int charToInt = (int) curChar;
                int position = charToInt - (int) 'a';
                a_frequences[position]++;
            }

            for (int i = 0; i < b.Length; i++)
            {
                var curChar = b[i];
                int charToInt = (int)curChar;
                int position = charToInt - (int)'a';
                b_frequences[position]++;
            }

            for (int i = 0; i < 26; i++)
            {
                int diff = Math.Abs(a_frequences[i] - b_frequences[i]);
                min_deletions += diff;
            }

            return min_deletions;
        }

    }
}
