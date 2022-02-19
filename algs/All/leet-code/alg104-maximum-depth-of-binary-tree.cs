using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using shared;


namespace maximumdepthofbinarytree
{
    // https://leetcode.com/problems/maximum-depth-of-binary-tree
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            var root = new TreeNode(3)
            {
                left = new(9),
                right = new(20)
                {
                    left = new(15),
                    right = new(7)
                }
            };

            var res = MaxDepth(root);
            Assert.AreEqual(3, res);
        }

        public int MaxDepth(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }
            return 1 + Math.Max(MaxDepth(root.left), MaxDepth(root.right));
        }

    }
}
