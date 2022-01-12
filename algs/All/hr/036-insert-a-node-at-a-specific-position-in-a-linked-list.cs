using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace insertanodeataspecificposition
{
    // https://www.hackerrank.com/challenges/insert-a-node-at-a-specific-position-in-a-linked-list

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
           
        }

        public static SinglyLinkedListNode insertNodeAtPosition(SinglyLinkedListNode llist, int data, int position)
        {
            var newNode = new SinglyLinkedListNode(data);
            var index = 0;
            var currentNode = llist;
            while (index < position -1)
            {
                currentNode = currentNode.next;
                index++;
            }

            newNode.next = currentNode.next;
            currentNode.next = newNode;

            return llist;
        }

    }

   public class SinglyLinkedListNode
    {
        public int data;
        public SinglyLinkedListNode next;

        public SinglyLinkedListNode(int nodeData)
        {
            this.data = nodeData;
            this.next = null;
        }
    }

    class SinglyLinkedList
    {
        public SinglyLinkedListNode head;
        public SinglyLinkedListNode tail;

        public SinglyLinkedList()
        {
            this.head = null;
            this.tail = null;
        }

        public void InsertNode(int nodeData)
        {
            SinglyLinkedListNode node = new SinglyLinkedListNode(nodeData);

            if (this.head == null)
            {
                this.head = node;
            }
            else
            {
                this.tail.next = node;
            }

            this.tail = node;
        }
    }

   
}
