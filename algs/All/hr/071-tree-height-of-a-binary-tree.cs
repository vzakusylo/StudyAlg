using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace templtreeheightofabinarytree
{
    // https://www.hackerrank.com/challenges/three-month-preparation-kit-tree-height-of-a-binary-tree/submissions

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var root = new Node(3)
            {
                left = new Node(2)
                {
                    data = 2,
                    left = new Node(1)
                },
                right = new Node(5){
                    left = new Node(4),
                    right = new Node(6)
                    {
                        right = new Node(7)
                    }
            }
            };

            var res = height(root);
            Assert.AreEqual(3, res);
        }

        public static int height(Node root)
        {
            // Write your code here.
            if (root == null) return 0; // whether its empty tree
            if (root.left == null && root.right == null) return 0; // whether its leaf node
            else if (root.left != null && root.right == null) // whether left tree is not null but right is null
            {
                return 1 + height(root.left);
            }
            else if (root.left == null & root.right != null) // whether 
            {
                return (1 + height(root.right));
            }
            else
            {
                return (1 + Math.Max(height(root.right), height(root.left))); 
            }
        }


    }

    public class Node
    {
        public Node left;
        public Node right;
        public int data;

        public Node(int data)
        {
            this.data = data;
            left = null;
            right = null;
        }

       // Node(){}
    }
}
