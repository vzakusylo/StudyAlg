using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace common_child
{
    //https://www.hackerrank.com/challenges/common-child

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var res = commonChild("ABCDEF", "FBDAMN");
            Assert.AreEqual(2, res); // BD  is the longest child of the given strings.

            res = commonChild("SHINCHAN", "NOHARAAA");
            Assert.AreEqual(3, res); // NHA
        }

        static int commonChild(string s1, string s2)
        {
            var child = 0;
            var maxChild = 0;
            for (int i = 0; i < s1.Length; i++)
            {
                var s1c = s1[i];
                var from = s2.IndexOf(s1[i]);
                if (from < 0)
                {
                    continue;
                }
                for (int j = from; j < s2.Length; j++)
                {                  
                    var s2c = s2[j];
                    if (!s2.Contains(s1c))
                    {
                        break;
                    }
                    if (s1c == s2c)
                    {
                        child++;
                        break;
                    }                    
                }
                if (maxChild < child)
                {
                    maxChild = child;
                    child = 0;
                }
            }
           
            return maxChild;
        }
    }
}
