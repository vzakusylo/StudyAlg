using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using shared;


namespace twosumiiinputarrayissorted
{
    // https://leetcode.com/problems/two-sum-ii-input-array-is-sorted/
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            var input = new[] {2, 7, 11, 15};
            var res = TwoSum(input, 9);
            Assert.AreEqual(1, res[0]);
            Assert.AreEqual(2, res[1]);
        }

        public int[] TwoSum(int[] numbers, int target)
        {
            int aPointer = 0;
            int bPointer = numbers.Count() - 1;

            while (aPointer <= bPointer)
            {
                int sum = numbers[aPointer] + numbers[bPointer];

                if (sum > target)
                {
                    bPointer -= 1;
                }
                else if (sum < target)
                {
                    aPointer += 1;
                }
                else
                {
                    return new int[] { aPointer + 1, bPointer + 1 };
                }

            }

            return new int[] { aPointer + 1, bPointer + 1 };
        }
    }
}
