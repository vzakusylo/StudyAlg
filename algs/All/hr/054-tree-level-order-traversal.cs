using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace treelevelordertraversal
{
    // https://www.hackerrank.com/challenges/tree-level-order-traversal

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var list = new List<int>() {1, 2, 5, 3, 6, 4};
            Node root = new Node(1);
            list.Skip(1).Select(x => insert(root, x)).ToArray();
            levelOrder(root);

        }

        public static void levelOrder(Node root)
        {

            if (root == null) return;

            LinkedList<Node> queue = new LinkedList<Node>();
            queue.AddLast(root);

            while (!queue.Any())
            {
               // poll() : This method retrieves and removes the head(first element) of this list.
               Node currentNode = queue.First();
                queue.RemoveFirst();

                Console.WriteLine(currentNode.data);
                if (currentNode.left != null)
                {
                    queue.AddLast(currentNode.left);
                }
                if (currentNode.right != null)
                {
                    queue.AddLast(currentNode.right);
                }
            }
        }

        public static Node insert(Node root, int data)
        {
            if (root == null)
            {
                return new Node(data);
            }
            else
            {
                Node cur;
                if (data <= root.data)
                {
                    cur = insert(root.left, data);
                    root.left = cur;
                }
                else
                {
                    cur = insert(root.right, data);
                    root.right = cur;
                }
                return root;
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
    }
}
