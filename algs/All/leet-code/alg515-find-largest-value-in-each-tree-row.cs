using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using shared;


namespace find_largest_value_in_each_tree_row
{
    // https://leetcode.com/problems/find-largest-value-in-each-tree-row/submissions/
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            TreeNode root = new TreeNode()
            {
                val = 1,
                left = new TreeNode()
                {
                    val = 3,
                    left = new TreeNode(){val = 5},
                    right = new TreeNode(){val = 3}
                },
                right = new TreeNode()
                {
                    val = 2,
                    right = new TreeNode(){val = 9}
                }
            };

            var res = LargestValues(root);

            Assert.AreEqual(1, res[0]);
            Assert.AreEqual(3, res[1]);
            Assert.AreEqual(9, res[2]);

        }

        public IList<int> LargestValues(TreeNode root)
        {
            var largestValues = new List<int>();
            HelperMethod(root, largestValues, 0);
            return largestValues;
        }

        public void HelperMethod(TreeNode root, List<int> largest, int level)
        {
            if (root == null) return;

            if (level == largest.Count)
            {
                largest.Add(root.val);
            }
            else
            {
                largest[level] = Math.Max(largest[level], root.val);
            }

            HelperMethod(root.left, largest, level + 1);
            HelperMethod(root.right, largest, level + 1);
        }

    }
}
