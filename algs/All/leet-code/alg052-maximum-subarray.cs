using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

//https://drive.google.com/file/d/0B7EfQdvL5qf_Q2JCWmZJRHQwdFU/view

namespace maximumsubarray
{
    // https://leetcode.com/problems/maximum-subarray/
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            var input = new [] {5, 4, -1, 7, 8};

            var res = MaxSubArray(input);
            Assert.AreEqual(23, res);
        }

        public int MaxSubArray(int[] nums)
        {
            if (nums == null || !nums.Any()) return 0;

            int result = nums[0];
            int max = result;

            for (int i = 1; i < nums.Count(); i++)
            {
                result = Math.Max(result + nums[i], nums[i]);
                max = Math.Max(result, max);
            }

            return max;

        }

    }
}
