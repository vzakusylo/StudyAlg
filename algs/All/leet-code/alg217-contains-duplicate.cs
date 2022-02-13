using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using shared;


namespace containsduplicate
{
    // https://leetcode.com/problems/contains-duplicate/
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            var input = new[] {1, 2, 3, 1};
            var res = ContainsDuplicate(input);
            Assert.IsTrue(res);
        }

        public bool ContainsDuplicate(int[] nums)
        {
            var hashSet = new Dictionary<int, int>();

            for (int i = 0; i < nums.Count(); i++)
            {
                if (hashSet.ContainsKey(nums[i]))
                {
                    return true;
                }
                else
                {
                    hashSet[nums[i]] = 1;
                }
            }

            return false;

        }
    }
}
