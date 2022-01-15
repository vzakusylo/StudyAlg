using System;
using System.Linq;
using insertanodeataspecificposition;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace getthevalueofthenodeataspecificpositionfromthetail
{
    // https://www.hackerrank.com/challenges/get-the-value-of-the-node-at-a-specific-position-from-the-tail

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var head = new SinglyLinkedListNode(1);
            head.next = new SinglyLinkedListNode(2);
            head.next.next = new SinglyLinkedListNode(3);
            head.next.next.next = new SinglyLinkedListNode(4);
            head.next.next.next.next = new SinglyLinkedListNode(5);
            head.next.next.next.next.next = new SinglyLinkedListNode(6);
            head.next.next.next.next.next.next = new SinglyLinkedListNode(7);
            head.next.next.next.next.next.next.next = new SinglyLinkedListNode(8);
            head.next.next.next.next.next.next.next.next = new SinglyLinkedListNode(9);
            head.next.next.next.next.next.next.next.next.next = new SinglyLinkedListNode(0);

            var res = getNode(head, 3);

            Assert.AreEqual(7, res);
        }

        public static int getNode(SinglyLinkedListNode head, int positionFromTail)
        {
            SinglyLinkedListNode pointerNode = head;
            int counter = 0;

            while (head != null)
            {
                head = head.next;
                if (counter < positionFromTail)
                {
                    counter++;
                }
                else
                {
                    pointerNode = pointerNode.next;
                }
            }

            return pointerNode.data;

        }

    }
}
