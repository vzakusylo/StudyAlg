using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace angram
{
    // https://www.hackerrank.com/challenges/making-anagrams

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var result = makingAnagrams("cde", "abc");
            Assert.AreEqual(4, result);

            result = makingAnagrams("absdjkvuahdakejfnfauhdsaavasdlkj", "djfladfhiawasdkjvalskufhafablsdkashlahdfa");
            Assert.AreEqual(19, result);
        }

        static int makingAnagrams(string s1, string s2)
        {
            HashSet<char> hashset = new HashSet<char>();
            foreach (char c in s1)              
                hashset.Add(c);            
            foreach (char c in s2)            
                hashset.Add(c);
            
            Dictionary<char, int> fs = hashset.Distinct().ToDictionary(x=> x, x=> 0);
            foreach (var c in s1.ToArray())            
                fs[c]++;    
            Dictionary<char, int> ss = hashset.Distinct().ToDictionary(x=>x,x=>0);
            foreach (var c in s2.ToArray())            
                ss[c]++;

            int res = 0;
            foreach (char c in hashset)
            {
                res += Math.Abs(fs[c] - ss[c]);
            }
            return res;
        }
    }
}
