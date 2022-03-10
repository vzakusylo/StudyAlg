using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using shared;


namespace binarysearch
{
    // https://leetcode.com/problems/binary-search/submissions/
    [TestClass]
    public class Solution
    {
       
        [TestMethod]
        public void Main()
        {
            var arr = new [] {-1, 0, 3, 5, 9, 12};
            var res = Search(arr, 9);
            Assert.AreEqual(4, res);
        }

        public int Search(int[] nums, int target)
        {
            if (!nums.Any()) return -1;

            int left = 0;
            int right = nums.Count() - 1;
            while (left <= right)
            {
                int midpoint = left + (right - left) / 2;
                if (nums[midpoint] == target)
                {
                    return midpoint;
                }
                else if (nums[midpoint] > target)
                {
                    right = midpoint - 1;
                }
                else
                {
                    left = midpoint + 1;
                }
            }

            return -1;
        }

    }
}
