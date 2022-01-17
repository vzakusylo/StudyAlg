using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace reverseadoublylinkedlist
{
    // https://www.hackerrank.com/challenges/reverse-a-doubly-linked-list

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
           
        }

        public static DoublyLinkedListNode reverse(DoublyLinkedListNode llist)
        {
            if (llist == null) return llist;

            DoublyLinkedListNode currentNode = llist;
            DoublyLinkedListNode newHead = llist;

            while (currentNode != null)
            {
                DoublyLinkedListNode prev = currentNode.prev;
                currentNode.prev = currentNode.next;
                currentNode.next = prev;
                newHead = currentNode;
                currentNode = currentNode.prev;
            }

            return newHead;
        }
    }

    public class DoublyLinkedListNode
    {
        public int data;
        public DoublyLinkedListNode next;
        public DoublyLinkedListNode prev;

        public DoublyLinkedListNode(int nodeData)
        {
            this.data = nodeData;
            this.next = null;
            this.prev = null;
        }
    }

    class DoublyLinkedList
    {
        public DoublyLinkedListNode head;
        public DoublyLinkedListNode tail;

        public DoublyLinkedList()
        {
            this.head = null;
            this.tail = null;
        }

        public void InsertNode(int nodeData)
        {
            DoublyLinkedListNode node = new DoublyLinkedListNode(nodeData);

            if (this.head == null)
            {
                this.head = node;
            }
            else
            {
                this.tail.next = node;
                node.prev = this.tail;
            }

            this.tail = node;
        }
    }
}
