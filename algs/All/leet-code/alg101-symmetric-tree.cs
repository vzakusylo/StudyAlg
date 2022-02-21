using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using shared;


namespace symmetrictree
{
    // https://leetcode.com/problems/symmetric-tree/submissions/
    [TestClass]
    public class Solution
    {
       
        [TestMethod]
        public void Main()
        {
            var root = new TreeNode(1)
            {
                left = new (2)
                {
                    left = new (3),
                    right = new(4)
                },
                right = new (2)
                {
                    left = new(4),
                    right = new(3)
                }
            };

            var res = IsSymmetric(root);
            Assert.IsTrue(res);
        }

        public bool IsSymmetric(TreeNode root)
        {
            return IsMirror(root, root);
        }

        public bool IsMirror(TreeNode t1, TreeNode t2)
        {
            if (t1 == null && t2 == null) return true;
            if (t1 == null || t2 == null) return false;

            return (t1.val == t2.val) && IsMirror(t1.left, t2.right) && IsMirror(t1.right, t2.left);
        }

    }
}
