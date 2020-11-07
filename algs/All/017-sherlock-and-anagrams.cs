using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace sherlock_and_anagrams
{
    //https://www.hackerrank.com/challenges/sherlock-and-anagrams

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var result = makingAnagrams("abba");
            Assert.AreEqual(4, result);

            result = makingAnagrams("abcd");
            Assert.AreEqual(0, result);

            result = makingAnagrams("cdcd");
            Assert.AreEqual(5, result);

            result = makingAnagrams("ifailuhkqq");
            Assert.AreEqual(3, result);

            result = makingAnagrams("kkkk");
            Assert.AreEqual(10, result);
        }

        static int makingAnagrams(string s)
        {
            Dictionary<string, int> map = new Dictionary<string, int>();

            for (int i = 0; i < s.Length; i++)
            {
                for (int j = i; j < s.Length; j++)
                {
                    char[] valC = s.Substring(i, j + 1 - i).ToCharArray();
                    Array.Sort(valC);
                    string val = new string(valC);
                    if (map.ContainsKey(val))
                        map[val] = map[val] + 1;
                    else
                        map.Add(val, 1);
                }
            }
            int anagramPairCount = 0;
            foreach (string key in map.Keys)
            {
                int n = map[key];
                anagramPairCount += (n * (n - 1)) / 2;
            }
           return anagramPairCount;
        }
    }
}
