using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using shared;


namespace twosum
{
    // https://leetcode.com/problems/two-sum/submissions/
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
           var nums = new int[]{2, 7, 11, 15 };
           var res = TwoSum(nums, 9);
           Assert.AreEqual(0, res[0]);
           Assert.AreEqual(1, res[1]);
        }

        public int[] TwoSum(int[] nums, int target)
        {
            for (int i = 0; i < nums.Count(); i++)
            {
                for (int j = i + 1; j < nums.Count(); j++)
                {
                    int complement = target - nums[i];

                    if (nums[j] == complement)
                    {
                        return new int[] { i, j };
                    }
                }
            }
            return new int[] { 0, 0 };
        }
    }
}
