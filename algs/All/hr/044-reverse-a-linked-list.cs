using System;
using System.Collections.Generic;
using System.Linq;
using insertanodeataspecificposition;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace reversealinkedlist
{
    // https://www.hackerrank.com/challenges/reverse-a-linked-list

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var head = new SinglyLinkedListNode(3);
            head.next = new SinglyLinkedListNode(2);
            head.next.next = new SinglyLinkedListNode(1);

            var res = reverse(head);

            Assert.AreEqual(1, res.data);
            Assert.AreEqual(2, res.next.data);
            Assert.AreEqual(3, res.next.next.data);
        }

        public static SinglyLinkedListNode reverse(SinglyLinkedListNode llist)
        {
            if (llist == null) return llist;

            SinglyLinkedListNode prev = null;
            SinglyLinkedListNode currentNode = llist;

            while (currentNode != null)
            {
                SinglyLinkedListNode nextNode = currentNode.next;
                currentNode.next = prev;
                prev = currentNode;
                currentNode = nextNode;
            }

            return prev;

        }
    }
}
