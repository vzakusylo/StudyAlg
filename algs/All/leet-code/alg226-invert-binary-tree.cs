using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using shared;


namespace invertbinarytree
{
    // https://leetcode.com/problems/invert-binary-tree/
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            var root = new TreeNode(4)
            {
                left = new(2)
                {
                    left = new(1),
                    right = new(3)
                },
                right = new(7)
                {
                    left = new(6),
                    right = new(9)
                }
            };

            var res = InvertTree(root);

            Assert.AreEqual(4, root.val);
            Assert.AreEqual(7, root.left.val);
            Assert.AreEqual(2, root.right.val);
            Assert.AreEqual(9, root.left.left.val);
            Assert.AreEqual(6, root.left.right.val);
            Assert.AreEqual(1, root.right.right.val);
            Assert.AreEqual(3, root.right.left.val);
        }

        public TreeNode InvertTree(TreeNode root)
        {
            if (root == null)
            {
                return root;
            }

            TreeNode left = InvertTree(root.left);
            TreeNode right = InvertTree(root.right);

            // swap
            root.right = left;
            root.left = right;

            return root;
        }
    }
}
