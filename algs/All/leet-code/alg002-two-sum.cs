using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace two_sum
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            var nums = new[] {2, 7, 11, 15};
            var target = 9;

            var res = TwoSum(nums, target);
            Assert.AreEqual(0, res[0]);
            Assert.AreEqual(1, res[1]);
        }

        public int[] TwoSumHashMap(int[] nums, int target)
        {
            Dictionary<int,int> map = new Dictionary<int,int>();
            for (int i = 0; i < nums.Length; i++)
            {
                int complement = target - nums[i];
                if (map.ContainsKey(complement))
                {
                    return new int[] {map[complement], i};
                }
                else
                {
                    map.Add(nums[i], i);
                }
            }

            return null;
        }

        public int[] TwoSum(int[] nums, int target)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = i+1; j < nums.Length; j++)
                {
                    int complement = target - nums[i];
                    if (nums[j] == complement)
                    {
                        return new[] {i, j};
                    }
                    
                }
            }
            return null;
        }
    }
}
