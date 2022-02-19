using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using shared;


namespace palindromenumber
{
    // https://leetcode.com/problems/palindrome-number/
    [TestClass]
    public class Solution
    {
       
        [TestMethod]
        public void Main()
        {
            var input = 121;
            var output = IsPalindrome(input);

            Assert.IsTrue(output);
        }

        public bool IsPalindrome(int x)
        {
            if (x == 0)
                return true;

            if (x < 0 || x % 10 == 0)
                return false;

            int reversedInt = 0;

            while (x > reversedInt)
            {
                int pop = x % 10;
                x /= 10;

                reversedInt = (reversedInt * 10) + pop;
            }

            if (reversedInt == x || x == reversedInt / 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
