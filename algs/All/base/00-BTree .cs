using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Base
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            BTree t = new BTree(3);

            t.Insert(10);
        }

        public class BTree
        {
            private BTreeNode root;
            private int degree;

            public BTree(int degree)
            {
                this.degree = degree;
                root = null;
            }

            public void Traverse()
            {
                if (root != null)
                {
                    root.Traverse();
                }
            }

            public BTreeNode Search(int key)
            {
                return root == null ? null : root.Search(key);
            }

            public void Insert(int key)
            {
                if (root == null)
                {
                    root = new BTreeNode(degree, true);
                    root.Keys[0] = key;
                    root.KeyCount = 1;
                }
                else
                {
                    if (root.KeyCount == 2 * degree - 1)
                    {
                        BTreeNode s = new BTreeNode(degree, false);
                        s.Children[0] = root;
                        s.SplitChild(0, root);
                        int i = 0;

                        if (s.Keys[0] < key)
                        {
                            i++;
                        }

                        s.Children[i].InsertNonFull(key);
                        root = s;
                    }
                    else
                    {
                        root.InsertNonFull(key);
                    }
                }
            }
        }

        public class BTreeNode
        {
            public int[] Keys { get; set; }
            public int Degree { get; set; }
            public BTreeNode[] Children { get; set; }
            public int KeyCount { get; set; }
            public bool IsLeaf { get; set; }

            public BTreeNode(int degree, bool isLeaf)
            {
                Degree = degree;
                IsLeaf = isLeaf;
                Keys = new int[2 * degree - 1];
                Children = new BTreeNode[2 * degree];
                KeyCount = 0;
            }

            public BTreeNode Search(int key)
            {
                int i = 0;
                while (i < KeyCount && key > Keys[i])
                {
                    i++;
                }

                if (Keys[i] == key)
                {
                    return this;
                }

                if (IsLeaf)
                {
                    return null;
                }

                return Children[i].Search(key);
            }

            public void Traverse()
            {
                int i;
                for (i = 0; i < KeyCount; i++)
                {
                    if (!IsLeaf)
                    {
                        Children[i].Traverse();
                    }
                    Console.Write($"{Keys[i]}");
                }

                if (!IsLeaf)
                {
                    Children[i].Traverse();
                }
            }

            public void SplitChild(int i, BTreeNode root)
            {
                
            }

            public void InsertNonFull(int key)
            {
                int i = KeyCount - 1;

                if (IsLeaf)
                {
                    while (i >= 0 && Keys[i] > key)
                    {
                        Keys[i + 1] = Keys[i];
                        i--;
                    }

                    Keys[i+1] = key;
                    KeyCount++;
                }
                else
                {
                    while (i >= 0 && Keys[i] > key)
                    {
                        i--;
                    }

                    if (Children[i + 1].KeyCount == 2 * Degree - 1)
                    {
                        SplitChild(i+1, Children[i+1]);

                        if (Keys[i + 1] < key)
                        {
                            i++;
                        }
                    }

                    Children[i+1].InsertNonFull(key);
                }
            }
        }
       
    }
}
