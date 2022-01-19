using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace isbinarysearchtree
{
    // https://www.hackerrank.com/challenges/is-binary-search-tree/problem?h_r=internal-search&isFullScreen=true

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var root = new Node
            {
                data = 3,
                right = new Node
                {
                    data = 2,
                    left = new Node { data = 6}
                },
                left = new Node
                {
                    data = 5, 
                    right = new Node {data = 4},
                    left = new Node {data = 1}
                }
            };

            var result = checkBST(root);

            Assert.AreEqual(false, result);

        }

        bool checkBST(Node root)
        {
            if (root == null) return true;
            if (root.left == null && root.right == null)
            {
                return true;
            }
            else if (root.left == null && root.right != null)
            {
                if (root.data < root.right.data)
                {
                    return checkBSTHelper(root.right, root.data, int.MaxValue);
                }
                else
                {
                    return false;
                }
            }
            else if (root.left != null && root.right == null)
            {
                if (root.data > root.left.data)
                {
                    return checkBSTHelper(root.left, int.MinValue, root.data);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (root.data > root.left.data && root.data < root.right.data)
                {
                    return checkBSTHelper(root.left, int.MinValue, root.data) &&
                           checkBSTHelper(root.right, root.data, int.MaxValue);
                }
                else
                {
                    return false;
                }
            }
        }

        bool checkBSTHelper(Node root, int minValue, int maxValue)
        {
            if (root == null) return true;

            return root.data > minValue && root.data < maxValue && checkBSTHelper(root.left, minValue, root.data) && checkBSTHelper(root.right, root.data, maxValue);
        }


    }

    public class Node
    {
        public int data;
        public Node left;
        public Node right;
    }
}
