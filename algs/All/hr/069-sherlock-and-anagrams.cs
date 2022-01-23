using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace sherlockandanagrams
{
    // https://www.hackerrank.com/challenges/sherlock-and-anagrams/problem

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var s = "abba";
            var res = sherlockAndAnagrams(s);
            Assert.AreEqual(4, res);
        }

        public static int sherlockAndAnagrams(string s)
        {
            int anagramPairs = 0;
            var hashMap = new Dictionary<string, int>();
            for (int i = 0; i < s.Count(); i++)
            {
                for (int j = i; j < s.Count(); j++)
                {
                    
                    char[] c = s.Substring(i, j + 1 - i).ToArray(); // !!!!!!!!!!!!!!!!!!!!!!!!
                    Array.Sort(c);
                    string k = new string(c);

                    if (hashMap.ContainsKey(k))
                    {
                        anagramPairs += hashMap[k];
                        hashMap[k] += 1;
                    }
                    else
                    {
                        hashMap[k] = 1;
                    }

                    //if (hashMap.ContainsKey(k))
                    //{
                    //    hashMap[k] += 1;
                    //}
                    //else
                    //{
                    //    hashMap[k] = 1;
                    //}
                }
            }

            //The number of combinations of n objects taken r at a time is determined by the following formula: C(n, r) = n!(n−r)! r

            //foreach (var k in hashMap.Keys)
            //{
            //    int v = hashMap[k];
            //    // 2 v = combination v! /(2! * (v-2)!)  // combinatorial formula
            //    anagramPairs += (v * (v - 1)) / 2;
            //}

            return anagramPairs;
        }
    }
}
