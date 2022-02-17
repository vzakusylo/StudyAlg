using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using shared;


namespace narytreepreordertraversal
{
    // https://leetcode.com/problems/n-ary-tree-preorder-traversal/
    [TestClass]
    public class Solution
    {

        /*
                     1
                   / | \
                  3  2  4
                 / \     
                5   6

            */

        [TestMethod]
        public void Main()
        {
            var root = new Node(1)
            {
                children = new List<Node>
                {
                    new (3)
                    {
                        children = new List<Node>()
                        {
                            new(5),
                            new(6)
                        }

                    },
                    new (2),
                    new(4)
                }
            };

            var res = Preorder(root);

            Assert.AreEqual(1, res[0]);
            Assert.AreEqual(3, res[1]);
            Assert.AreEqual(5, res[2]);
            Assert.AreEqual(6, res[3]);
            Assert.AreEqual(2, res[4]);
            Assert.AreEqual(4, res[5]);
        }

        public IList<int> Preorder(Node root)
        {
            List<Node> stack = new List<Node>();
            List<int> outputArr = new List<int>();

            if (root == null)
            {
                return outputArr.ToList();
            }

            stack.Add(root);
            while (stack.Any())
            {
                Node node = stack.Last();
                stack.RemoveAt(stack.Count -1);
                
                outputArr.Add(node.val);
                var reversChildren = node.children?.Reverse() ?? new List<Node>();
                foreach (Node child in reversChildren)
                {
                    stack.Add(child);
                }
            }

            return outputArr.ToList();
        }
    }
}
