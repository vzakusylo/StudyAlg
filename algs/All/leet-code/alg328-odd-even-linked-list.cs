using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using shared;


namespace oddevenlinkedlist
{
    // https://leetcode.com/problems/odd-even-linked-list/
    [TestClass]
    public class Solution
    {
       
        [TestMethod]
        public void Main()
        {
            ListNode head = new ListNode(1)
            {
                next = new ListNode(2)
                {
                    next = new ListNode(3)
                    {
                        next = new ListNode(4)
                        {
                            next = new ListNode(5)
                        }
                    }
                }
            };


            var oddEvenList = OddEvenList(head);

            Assert.AreEqual(1, oddEvenList.val);
            Assert.AreEqual(3, oddEvenList.next.val);
            Assert.AreEqual(5, oddEvenList.next.next.val);
            Assert.AreEqual(2, oddEvenList.next.next.next.val);
            Assert.AreEqual(4, oddEvenList.next.next.next.next.val);
        }

        public ListNode OddEvenList(ListNode head)
        {
            if (head == null) return head;

            ListNode odd = head;
            ListNode even = head.next;
            ListNode evenHead = even;

            while (even != null && even.next != null)
            {
                odd.next = even.next;
                odd = odd.next;
                even.next = odd.next;
                even = even.next;
            }

            odd.next = evenHead;
            return head;
        }

    }
}
