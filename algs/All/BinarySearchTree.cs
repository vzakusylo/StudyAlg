using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinarySearchTree
{
//https://www.geeksforgeeks.org/find-closest-element-binary-search-tree/
//       9
//    4     17
//  3    6     22
//     5   7  20
//?????????????????????? 18 
    [TestClass]
    public class Solution
    {
        public static int min_diff, min_diff_key; 

        [TestMethod]
        public void Main()
        {
            Nodi root = new Nodi(9);
            root.Left = new Nodi(4);
            root.Right = new Nodi(17);
            root.Left.Left = new Nodi(3);
            root.Left.Right = new Nodi(6);
            root.Left.Right.Left = new Nodi(5);
            root.Left.Right.Right = new Nodi(7);
            root.Right.Right = new Nodi(22);
            root.Right.Right.Left = new Nodi(20);
            
            Console.WriteLine($"k=4 result={maxDiff(root, 4)} expected=17");
            Console.WriteLine($"k=18 result={maxDiff(root, 18)} expected=17");
            Console.WriteLine($"k=12 result={maxDiff(root, 12)} expected=9");
        }

        private int maxDiff(Nodi root, int k)
        {
            min_diff = 9999999;
            min_diff_key = -1;

            maxDiffUtil(root, k);

            return min_diff;
        }

        private static void maxDiffUtil(Node<int> ptr, int k)
        {
            if (ptr == null)
            {
                return;
            }
            if (ptr.Key == k)
            {
                min_diff_key = k;
                return;
            }
            if (min_diff > Math.Abs(ptr.Key - k))
            {
                min_diff = Math.Abs(ptr.Key - k);
                min_diff_key = ptr.Key;
            }
            if (k < ptr.Key)
            {
                maxDiffUtil(ptr.Left, k);
            }
            else
            {
                maxDiffUtil(ptr.Right, k);
            }
        }
    }

    public class Nodi : Node<int>
    {
        public Nodi(int t) : base(t)
        {
        }        
    }

    public class Node<T>
    {
        public T Key { get; set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }

        public Node(T t)
        {
            Key = t;
            Left = Right = null;
        }

        public static Node<T> NewNode(T key)
        {
            Node<T> node = new Node<T>(key);
            node.Key = key;
            node.Left = node.Right = null;
            return node;
        }
    }   
}
