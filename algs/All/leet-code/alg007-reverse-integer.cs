using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using shared;


namespace reverseinteger
{
    // https://leetcode.com/problems/reverse-integer/
    [TestClass]
    public class Solution
    {
       
        [TestMethod]
        public void Main()
        {
            var input = 123;
            var output = Reverse(input);

            Assert.AreEqual(321, output);
        }

        public int Reverse(int x)
        {
            int reversed = 0;
            int pop; // last digit

            // 123 / 10 = 12

            while (x != 0)
            {
                pop = x % 10;
                x /= 10;

                if (reversed > int.MaxValue / 10 || reversed == int.MaxValue / 10 && pop > 7)
                {
                    return 0;
                }

                if (reversed < int.MinValue / 10 || reversed == int.MinValue / 10 && pop < -8)
                {
                    return 0;
                }

                reversed = (reversed * 10) + pop;
            }

            return reversed;
        }

    }
}
