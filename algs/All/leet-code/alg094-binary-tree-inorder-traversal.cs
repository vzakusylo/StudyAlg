using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using shared;


namespace binarytreeinordertraversal
{
    // https://leetcode.com/
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            /*
                     1
                    / \
                       2
                      / \ 
                     3
            */
            var root = new TreeNode(1)
            {
                right = new(2)
                {
                    left = new(3)
                }
            };

            var res = InorderTraversal(root);
            Assert.AreEqual(1, res[0]);
            Assert.AreEqual(3, res[1]);
            Assert.AreEqual(2, res[2]);
        }

        public IList<int> InorderTraversal(TreeNode root)
        {
            Stack<TreeNode> stack = new Stack<TreeNode>();
            List<int> outputArr = new List<int>();

            if (root == null)
            {
                return outputArr;
            }

            TreeNode current = root;
            while (current != null || stack.Count() != 0)
            {
                while (current != null)
                {
                    stack.Push(current);
                    current = current.left;
                }

                current = stack.Pop();
                outputArr.Add(current.val);
                current = current.right;
            }

            return outputArr;
        }
    }
}
