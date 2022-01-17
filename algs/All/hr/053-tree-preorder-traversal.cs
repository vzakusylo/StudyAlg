using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace treepreordertraversal
{
    // https://www.hackerrank.com/challenges/tree-preorder-traversal

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            //preorder - root->left->right
            //inorder - left->root->right
            //postorder - left->right->root

            //   1
            //    \
            //     2
            //      \
            //      5
            //    /  \
            //   3    6
            //    \
            //     4

            Node root = new Node(1);
            root.right = new Node(2);
            root.right.right = new Node(5);
            root.right.right.right = new Node(6);
            root.right.right.left = new Node(3);
            root.right.right.left.right = new Node(4);

            var res = new List<int>();
            preOrderR(root, res);

            Assert.AreEqual("1 2 3 4 5 6", string.Join(' ', res));

            res = preOrder(root).ToList();
            Assert.AreEqual("1 2 3 4 5 6", string.Join(' ', res));

        }

        public static void preOrderR(Node root, List<int> res)
        {
            if (root == null) return;

            res.Add(root.data);
            Console.WriteLine(root.data + " ");
            preOrderR(root.left, res);
            preOrderR(root.right, res);
        }
        
        public static int [] preOrder(Node root)
        {
            if (root == null) return Array.Empty<int>();
            var res = new List<int>();

            Stack<Node> stack = new Stack<Node>();
            stack.Push(root);
            while (!stack.Any())
            {
                Node currentNode = stack.Pop();
                Console.WriteLine(currentNode.data + " ");
                res.Add(currentNode.data);
                if (currentNode.right != null)
                {
                    stack.Push(currentNode.right);
                }
                if (currentNode.left != null)
                {
                    stack.Push(currentNode.left);
                }
            }

            return res.ToArray();

            // System.out.print(root.data + " ");
            // preOrder(root.left);
            // preOrder(root.right);

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

    }
}
