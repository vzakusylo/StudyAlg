using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinaryTree
{
    [TestClass]
    public class Template
    {
        [TestMethod]
        public void Main()
        {
        }

        [TestMethod]
        public void RootIsInitiatedWhenOnlyOneElement()
        {
            Tree theTree = new Tree(new Node()
            {
                Key = 1
            });

            Assert.IsNotNull(theTree.Root);
            Assert.IsNull(theTree.Root.Left);
            Assert.IsNull(theTree.Root.Right);
        }

        [TestMethod]
        public void InsertFirstLeft()
        {
            Tree theTree = new Tree(new Node()
            {
                Key = 5
            });

            theTree.Insert(4);

            Assert.IsNotNull(theTree.Root);
            Assert.AreEqual(5, theTree.Root.Key);
            Assert.IsNotNull(theTree.Root.Left);
            Assert.AreEqual(4, theTree.Root.Left.Key);
            Assert.IsNull(theTree.Root.Right);
        }

        [TestMethod]
        public void InsertFirstRight()
        {
            Tree theTree = new Tree(new Node()
            {
                Key = 5
            });

            theTree.Insert(6);

            Assert.IsNotNull(theTree.Root);
            Assert.AreEqual(5, theTree.Root.Key);
            Assert.IsNotNull(theTree.Root.Right);
            Assert.AreEqual(6, theTree.Root.Right.Key);
            Assert.IsNull(theTree.Root.Left);
        }
    }

    public class Node
    {
        public int Key { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }

    public class Tree
    {
        private Node root;
        public Node Root {get {return root;} }

        public Tree(Node root)
        {
            this.root = root;
        }
        public Node Find(int key)
        {
            Node current = root;
            while (current.Key != key)
            {
                if (key < current.Key)
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

        public void Insert(int id)
        {
            Node newNode = new Node();
            newNode.Key = id;
            if (root == null)
            {
                root = newNode;
            }
            else
            {
                Node current = root;
                Node parrent;
                while (true)
                {
                    parrent = current;
                    if (id < current.Key)
                    {
                        current = current.Left;
                        if (current == null)
                        {
                            parrent.Left = newNode;
                            return;
                        }
                    }
                    if (id > current.Key)
                    {
                        current = current.Right;
                        if (current == null)
                        {
                            parrent.Right = newNode;
                            return;
                        }
                    }
                }

            }
        }
    }
}
