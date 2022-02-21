using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using shared;


namespace pathsum
{
    // https://leetcode.com/problems/path-sum/
    [TestClass]
    public class Solution
    {
       
        [TestMethod]
        public void Main()
        {
            var root = new TreeNode(5)
            {
                left = new(4)
                {
                    left = new(11)
                    {
                        left = new(7),
                        right = new(2)
                    }
                },
                right = new (8)
                {
                    left = new(13),
                    right = new (4)
                    {
                        right = new(1)
                    }
                }
            };

            var res = HasPathSum(root, 22);

            Assert.IsTrue(res);

        }

        public bool HasPathSum(TreeNode root, int targetSum)
        {
            if (root == null) return false;

            Stack<TreeNode> nodeStack = new Stack<TreeNode>();
            Stack<int> sumStack = new Stack<int>();

            nodeStack.Push(root);
            sumStack.Push(targetSum - root.val);

            while (nodeStack.Count > 0)
            {
                TreeNode node = nodeStack.Pop();
                int currSum = sumStack.Pop();

                if (node.left == null && node.right == null && currSum == 0)
                {
                    return true;
                }

                if (node.left != null)
                {
                    nodeStack.Push(node.left);
                    sumStack.Push(currSum - node.left.val);
                }
                if (node.right != null)
                {
                    nodeStack.Push(node.right);
                    sumStack.Push(currSum - node.right.val);
                }
            }

            return false;
        }

    }
}
