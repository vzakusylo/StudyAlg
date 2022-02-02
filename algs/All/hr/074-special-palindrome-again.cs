using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace specialpalindromeagain
{
    // https://www.hackerrank.com/challenges/special-palindrome-again/problem?h_r=internal-search&isFullScreen=true

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var res = substrCount("abcbaba");
            //res = substrCount1(7, "abcbaba");
            Assert.AreEqual(10, res);
        }

        static long substrCount(string s)
        {
            long retVal = s.Length;

            for (int i = 0; i < s.Length; i++)
            {
                var startChar = s[i];
                int diffCharIdx = -1;
                for (int j = i + 1; j < s.Length; j++)
                {
                    var currChar = s[j];
                    if (startChar == currChar)
                    {
                        if ((diffCharIdx == -1) ||
                            (j - diffCharIdx) == (diffCharIdx - i))
                            retVal++;
                    }
                    else
                    {
                        if (diffCharIdx == -1)
                            diffCharIdx = j;
                        else
                            break;
                    }
                }
            }
            return retVal;
        }

        static long substrCount1(int n, string s)
        {
            int counter = 0;
            for (int i = 0; i < s.Length; i++)
            {
                counter++;
                for (int j = i + 1; j < s.Count(); j++)
                {
                    if (s[i] != s[j])
                    { // aabaa
                        
                        if (2 * j - 1 +1 < s.Count())
                        {
                            var s1 = s.Substring(i, s.Length - j);
                            
                            var fromIndex = j + 1;
                            var toIndex = 2 * j - i + 1;
                           
                            var s2 = s.Substring(fromIndex, s.Length - toIndex);

                            if (s1 == s2)
                            {
                                counter++;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        counter++;
                    }

                }
            }

            return counter;

        }


    }
}
