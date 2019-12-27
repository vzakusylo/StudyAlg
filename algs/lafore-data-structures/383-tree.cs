using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tree
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            // 4 lelvels filling
            Tree t = new Tree();
            t.Insert(8);
            t.Insert(4);
            t.Insert(12);
            t.Insert(2);
            t.Insert(6);
            t.Insert(10);
            t.Insert(14);
            t.Insert(1);
            t.Insert(3);
            t.Insert(5);
            t.Insert(7);
            t.Insert(9);
            t.Insert(11);
            t.Insert(13);
            t.Insert(15);

            t.DisplayTree();
//==========================================================
//                          8
//              4                         12
//        2           6           10             14
//    1      3     5     7     9      11     13      15
//==========================================================
        }
    }

    public class Tree
    {
        private Node Root;

        public Tree()
        {
            Root = null;
        }

        public void DisplayTree()
        {
            Stack globalStack = new Stack();
            globalStack.Push(Root);
            int nBlanks = 32;
            bool isRowEmpty = false;
            Console.WriteLine("================================");
            while (isRowEmpty == false)
            {
                Stack localStack = new Stack();
                isRowEmpty = true;
                for (int j = 0; j < nBlanks; j++)
                {
                    Console.Write(" ");
                }

                while (globalStack.Count != 0)
                {
                    Node temp = (Node)globalStack.Pop();
                    if (temp != null)
                    {
                        Console.Write(temp.Data);
                        localStack.Push(temp.Left);
                        localStack.Push(temp.Right);

                        if (temp.Left != null || temp.Right != null)
                        {
                            isRowEmpty = false;
                        }
                    }
                    else
                    {
                        Console.Write("--");
                        localStack.Push(null);
                        localStack.Push(null);
                    }
                    for (int j = 0; j < nBlanks * 2 - 2; j++)
                    {
                        Console.Write("  ");
                    }                   
                }
                Console.WriteLine();
                nBlanks /= 2;
                while (localStack.Count != 0)
                {
                    globalStack.Push(localStack.Pop());
                }
            }
            Console.WriteLine("================================");
        }

        public Node Find(int key)
        {
            Node current = Root;
            while (current.Data != key)
            {
                if (key < current.Data)
                {
                    current = current.Left;
                }
                else
                {
                    current = current.Right;
                }

                if (current == null)
                {
                    return null;
                }
            }

            return current;
        }

        internal void Insert(int d)
        {
            Node newNode = new Node();
            newNode.Data = d;

            if (Root == null)
            {
                Root = newNode;
            }
            else
            {
                Node currentNode = Root;
                Node parent;
                while (true)
                {
                    parent = currentNode;
                    if (d < currentNode.Data)
                    {
                        currentNode = currentNode.Left;
                        if (currentNode == null)
                        {
                            parent.Left = newNode;
                            return;
                        }
                    }
                    else
                    {
                        currentNode = currentNode.Right;
                        if (currentNode == null)
                        {
                            parent.Right = newNode;
                            return;
                        }
                    }
                }
            }
        }

        public class Node
        {
            public int Data { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public void DisplayNode()
            {
                Console.WriteLine($"{Data}");
            }
        }
    }
}
