using System;
using System.Linq;
using insertanodeataspecificposition;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace getthevalueofthenodeataspecificpositionfromthetail1
{
    // https://www.hackerrank.com/challenges/get-the-value-of-the-node-at-a-specific-position-from-the-tail/problem

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var list = new SinglyLinkedListNode(1);
            list.next = new SinglyLinkedListNode(4);
            list.next.next = new SinglyLinkedListNode(4);
            list.next.next.next = new SinglyLinkedListNode(3);
            list.next.next.next.next = new SinglyLinkedListNode(2);
            list.next.next.next.next.next = new SinglyLinkedListNode(1);
            list.next.next.next.next.next.next = new SinglyLinkedListNode(2);

            var res = getNode(list, 3);
            Assert.AreEqual(3, res);
        }

        public static int getNode(SinglyLinkedListNode head, int positionFromTail)
        {
            SinglyLinkedListNode fast = head;
            int index = 0;
            while (fast != null)
            {
                if (index == positionFromTail) break;

                index++;
                fast = fast.next;
            }

            SinglyLinkedListNode slow = head;
            SinglyLinkedListNode parent = null;
            while (fast != null)
            {
                parent = slow;
                slow = slow.next;
                fast = fast.next;
            }

            return parent.data;

        }

    }
}
