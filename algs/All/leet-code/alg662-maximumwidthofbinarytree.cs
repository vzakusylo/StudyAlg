using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Concurrent;
using shared;


namespace maximumwidthofbinarytree
{
    // https://leetcode.com/problems/maximum-width-of-binary-tree/submissions/883649100/
    [TestClass]
    public class Solution
    {
        int _maxWidth;
        readonly ConcurrentDictionary<int, int> _leftMostPositions = new();

        private int WidthOfBinaryTree(TreeNode root)
        {
            GetWidth(root, 0, 0);
            return _maxWidth;
        }

        private void GetWidth(TreeNode root, int depth, int position)
        {
            if (root == null) return;

            _leftMostPositions.GetOrAdd(depth, k => position);
            _maxWidth = Math.Max(_maxWidth, position - _leftMostPositions[depth] + 1);
            GetWidth(root.left, depth + 1, position * 2);
            GetWidth(root.right, depth + 1, position * 2 + 1);
        }

        [TestMethod]
        public void Main()
        {
            var root = new TreeNode
            {
                val = 1,
                left = new TreeNode
                {
                    val = 3,
                    left = new TreeNode { val = 5},
                    right = new TreeNode { val = 3}
                },
                right = new TreeNode
                {
                    val = 2,
                    right = new TreeNode { val = 9 }
                }  
            };
            var res =WidthOfBinaryTree(root);

            Assert.AreEqual(4, res);
        }
      
    }
}
