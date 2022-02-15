using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using shared;


namespace narytreepostordertraversal
{
    // https://leetcode.com/problems/n-ary-tree-postorder-traversal/
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {

            /*
                     1
                   / | \
                  3  2  4
                 / \     
                5   6

            */

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

            var res = Postorder(root);

            // [5,6,3,2,4,1]
            Assert.AreEqual(5, res[0]);
            Assert.AreEqual(6, res[1]);
            Assert.AreEqual(1, res[5]);
        }

        public IList<int> Postorder(Node root)
        {
            // post left right root
            // preorder root left right
            // inorder left root right 

            Stack<Node> stack = new Stack<Node>();
            Stack<int> outputArray = new Stack<int>();

            if (root == null)
            {
                return outputArray.ToArray();
            }

            stack.Push(root);
            while (stack.Count() != 0)
            {
                Node node = stack.Pop();
                outputArray.Push(node.val);
                if (node.children is null) continue;
                foreach (Node child in node.children)
                {
                    stack.Push(child);
                }
            }

            return outputArray.ToArray();
        }
    }
}
