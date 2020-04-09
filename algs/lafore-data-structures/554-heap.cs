using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Heap
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            int value, value2;
            Heap theHeap = new Heap(31);
            bool success;

            theHeap.Insert(70);
            theHeap.Insert(40);
            theHeap.Insert(50);
            theHeap.Insert(20);
            theHeap.Insert(60);
            theHeap.Insert(100);
            theHeap.Insert(80);
            theHeap.Insert(30);
            theHeap.Insert(10);
            theHeap.Insert(90);

            var choise = "s" ; // s-enter, i-insert, r-remove, c-change priority
            switch (choise)
            {
                case "s":
                    theHeap.Display();
                    break;
                case "i":
                    var newValue = (new Random()).Next(1, 100);
                    success = theHeap.Insert(newValue);
                    if (!success)
                    {
                        Console.WriteLine("Can't insert: the heap is full");
                        break;
                    }
                    break;
                case "r":
                    if(!theHeap.IsEmpty())
                    {
                        theHeap.Remove();
                    }
                    else
                    {
                        Console.WriteLine("Can't remove: the heap full");
                    }
                    break;
                case "c":
                    value = (new Random()).Next(1, 5);
                    value2 = (new Random()).Next(2, 3);
                    success = theHeap.Change(value, value2);
                    if (!success)
                    {
                        Console.WriteLine("Invalid index");
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public class Node
    {
        public int Key { get; set; }

        public Node(int key)
        {
            Key = key;
        }
    }

    public class Heap
    {
        private int v;
        private Node[] heapArray;
        private int maxSize;
        private int currentSize;

        public Heap(int mx)
        {
            maxSize = mx;
            currentSize = 0;
            heapArray = new Node[maxSize];
        }

        internal bool Change(int value, int value2)
        {
            throw new NotImplementedException();
        }

        internal void Display()
        {
            throw new NotImplementedException();
        }

        internal bool Insert(int key)
        {
            if (currentSize == maxSize)
            {
                return false;
            }
            Node newNode = new Node(key);
            heapArray[currentSize] = newNode;
            TrickleUp(currentSize++);
            return true;
        }

        private void TrickleUp(int index)
        {
            int parent = (index - 1) / 2;
            Node bottom = heapArray[index];
            while (index > 0 && heapArray[parent].Key < bottom.Key)
            {
                heapArray[index] = heapArray[parent];
                index = parent;
                parent = (parent - 1) / 2;
            }
            heapArray[index] = bottom;
        }

        internal bool IsEmpty()
        {
            return currentSize == 0;
        }

        internal Node Remove()
        {
            Node root = heapArray[0];
            heapArray[0] = heapArray[--currentSize];
            TrickleDown(0);
            return root;
        }

        private void TrickleDown(int v)
        {
            throw new NotImplementedException();
        }
    }
}
