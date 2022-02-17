using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using shared;


namespace mergetwobinarytrees
{
    // https://leetcode.com/problems/merge-two-binary-trees/
    [TestClass]
    public class Solution
    {
        /*                 Tree1    Tree2        Sum
                            1        2            3
                           / \      / \         /   \ 
                          3   2    1   3       4     5
                         /          \   \     / \     \
                        5            4   7   5   4     7

        */
        [TestMethod]
        public void Main()
        {
            var root1 = new TreeNode(1)
            {
                right = new(2),
                left = new(3)
                {
                    left = new(5)
                }
            };
            var root2 = new TreeNode(2)
            {
                right = new(3)
                {
                    right = new(7)
                },
                left = new(1)
                {
                    right = new(4)
                }
            };

            var res = MergeTrees(root1, root2);

            Assert.AreEqual(3, res.val);
            Assert.AreEqual(4, res.left.val);
            Assert.AreEqual(5, res.left.left.val);
            Assert.AreEqual(4, res.left.right.val);
            Assert.AreEqual(5, res.right.val);
            Assert.AreEqual(7, res.right.right.val);
        }

        public TreeNode MergeTrees(TreeNode root1, TreeNode root2)
        {
            if (root1 == null)
            {
                return root2;
            }
            if (root2 == null)
            {
                return root1;
            }
            root1.val += root2.val;
            root1.left = MergeTrees(root1.left, root2.left);
            root1.right = MergeTrees(root1.right, root2.right);

            return root1;
        }
    }
}
