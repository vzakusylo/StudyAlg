using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoreLinq.Extensions;

namespace BinarySearchTreeLowestCommonAncestor
{
    // https://www.hackerrank.com/challenges/binary-search-tree-lowest-common-ancestor

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            Node root = null;
            var arr = new[] {4, 2, 3, 1, 7, 6};
            arr.ForEach(x => { root = insert(root, x); });
            Node ans = lca(root, 1, 7);
            Assert.AreEqual(4, ans.data);

            root = null;
            arr = new[] { 5,3,8,2,4,6,7 };
            arr.ForEach(x => { root = insert(root, x); });
            ans = lca(root, 7, 3);
            Assert.AreEqual(5, ans.data);
        }

        public static Node lca(Node root, int v1, int v2)
        {
            if (v1 > root.data && v2 > root.data)
            {
                return lca(root.right, v1, v2);
            }

            if (v1 < root.data && v2 < root.data)
            {
                return lca(root.left, v1, v2);
            }

            return root;
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
}
