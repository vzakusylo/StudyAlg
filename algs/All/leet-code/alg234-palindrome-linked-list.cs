using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using shared;


namespace palindromelinkedlist
{
    // https://leetcode.com/problems/palindrome-linked-list/
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            var head = new ListNode(1)
            {
                next = new ListNode(2)
                {
                    next = new ListNode(2)
                    {
                        next = new ListNode(1)
                    }
                }
            };

            var res = IsPalindrome(head);

            Assert.IsTrue(res);
        }

        public bool IsPalindrome(ListNode head)
        {
            ListNode slow = head;
            ListNode fast = head;

            while (fast != null && fast.next != null)
            {
                fast = fast.next.next;
                slow = slow.next;
            }

            slow = reversed(slow);
            fast = head;

            while (slow != null)
            {
                if (slow.val != fast.val)
                {
                    return false;
                }
                slow = slow.next;
                fast = fast.next;
            }

            return true;
        }

        public ListNode reversed(ListNode head)
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

