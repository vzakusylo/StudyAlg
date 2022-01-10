using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RepeatedString
{
    //https://www.hackerrank.com/challenges/common-child

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var s = "aba";
            var res = RepeatedString(s, 10);
            Assert.AreEqual(7, res);
        }

        public static long RepeatedString(string s, long n)
        {
            long numOfA = 0;
            var fullStrNum = n / s.Length;
            var lastStrNum = n % s.Length;

            if (fullStrNum > 0)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] == 'a')
                    {
                        numOfA++;
                    }
                }

                numOfA = numOfA * fullStrNum;
            }

            if (lastStrNum > 0)
            {
                for (int i = 0; i < lastStrNum; i++)
                {
                    if (s[i] == 'a')
                    {
                        numOfA++;
                    }
                }
            }

            return numOfA;
        }
    }
}
        
    

