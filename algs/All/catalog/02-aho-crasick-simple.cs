using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AhoCorasickSimple
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            var app = new AhoCorasickSimple();
            var res =app.BuildAutomation(new[] { "apple", "bitcoin", "dollar"});
        }
    }


    public class AhoCorasickSimple
    {
        static int ALPHABET_SIZE = 26;
        String[] prefixes;

        public int [,] BuildAutomation(string [] words)
        {
            Dictionary<string, int> prefixMap = new Dictionary<string, int>();
            foreach (var s in words)
                for (int i = 0; i <= s.Length; i++)
                {
                    var newKey = s.Substring(0, i);
                    if(!prefixMap.ContainsKey(newKey))
                        prefixMap.Add(newKey, 0);
                }

            prefixes = prefixMap.Keys.Select(x=>x).ToArray();
            for (int i = 0; i < prefixes.Length; i++)
            {
                prefixMap[prefixes[i]] = i;
            }

            int[,] transitions = new int[prefixes.Length, ALPHABET_SIZE];
            for(int i = 0; i < prefixes.Length; i++)
            {
                for (int j = 0; j < ALPHABET_SIZE; j++)
                {
                    var s = prefixes[i] + (char)('a' + j);
                    while (!prefixMap.ContainsKey(s) /*&& !string.IsNullOrEmpty(s)*/)
                    {
                        s = s.Substring(1);
                    }
                    transitions[i, j] = prefixMap[s];
                }
            }

            return transitions;
            
        }
    }
}
