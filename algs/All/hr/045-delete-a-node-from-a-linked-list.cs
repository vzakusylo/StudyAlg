using System;
using System.Collections.Generic;
using System.Linq;
using insertanodeataspecificposition;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace deleteanodefromalinkedlist
{
    // https://www.hackerrank.com/challenges/delete-a-node-from-a-linked-list

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var head = new SinglyLinkedListNode(3);
            head.next = new SinglyLinkedListNode(2);
            head.next.next = new SinglyLinkedListNode(1);
            head.next.next.next = new SinglyLinkedListNode(4);

            // index   0   1    2    3
            // before  3-> 2 -> 1 -> 4
            // after   3-> 2 ->      4
            var res = deleteNode(head, 2);

            Assert.AreEqual(3, res.data);
            Assert.AreEqual(2, res.next.data);
            Assert.AreEqual(4, res.next.next.data);
        }

        public static SinglyLinkedListNode deleteNode(SinglyLinkedListNode llist, int position)
        {
            if (llist == null) return llist;
            if (position == 0) return llist.next;

            int counter = 0;
            SinglyLinkedListNode currentNode = llist;
            while (counter < position - 1)
            {
                currentNode = currentNode.next;
                counter++;
            }
            currentNode.next = currentNode.next.next;
            return llist;

        }
    }
}
