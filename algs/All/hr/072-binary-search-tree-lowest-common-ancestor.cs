using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace binarysearchtreelowestcommonancestor
{
    // https://www.hackerrank.com/challenges/binary-search-tree-lowest-common-ancestor/problem

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var root = new Node(4)
            {
                left = new Node(2)
                {
                    left = new Node(1),
                    right = new Node(3)
                },
                right = new Node(7)
                {
                    left = new Node(6)
                }
            };

            var res = lca(root, 1, 7);


            Assert.AreEqual(4, res.data);
        }

        public static Node lca(Node root, int v1, int v2)
        {
            // Write your code here.

            Node current = root;
            List<Node> path1 = getPath(current, v1);

            current = root;
            List<Node> path2 = getPath(current, v2);

            int index = 0;
            Node lcaNode = null;
            while (index < path1.Count && index < path2.Count)
            {
                if (path1[index].data == path2[index].data)
                {
                    lcaNode = path1[index];
                }

                index++;
            }

            return lcaNode;
        }

        public static List<Node> getPath(Node root, int v)
        {
            var path = new List<Node>();
            while (root != null)
            {
                path.Add(root);

                if (root.data == v)
                {
                    break;
                }
                else if (root.data < v)
                {
                    root = root.right;
                }
                else
                {
                    root = root.left;
                }
            }

            return path;
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