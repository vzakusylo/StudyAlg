using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using shared;

namespace reverselinkedlist
{
    // https://leetcode.com/problems/reverse-linked-list/
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            var input = new ListNode(1)
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

            var res = ReverseList(input);
            Assert.AreEqual(5, res.val);
            Assert.AreEqual(4, res.next.val);
            Assert.AreEqual(3, res.next.next.val);
        }

        public ListNode ReverseList(ListNode head)
        {
            ListNode prev = null;

            while (head != null)
            {
                ListNode next = head.next;
                head.next = prev;
                prev = head;
                head = next;
            }

            return prev;
        }
    }
}
