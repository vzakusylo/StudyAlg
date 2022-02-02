using System;
using System.Linq;
using insertanodeataspecificposition;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace reversealinkedlist2
{
    // https://www.hackerrank.com/challenges/one-month-preparation-kit-reverse-a-linked-list/problem?isFullScreen=true

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

        public static SinglyLinkedListNode reverse(SinglyLinkedListNode head)
        {
            // Write your code here
            if (head == null || head.next == null) return head;

            SinglyLinkedListNode pre = null;
            SinglyLinkedListNode current = head;
            SinglyLinkedListNode post = current.next;

            while (post != null)
            {
                current.next = pre;
                pre = current;
                current = post;

                post = post.next;
            }

            current.next = pre;

            return current;

        }


    }
}
