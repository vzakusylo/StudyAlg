using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using shared;


namespace linkedlistcycle
{
    // https://leetcode.com/problems/linked-list-cycle
    [TestClass]
    public class Solution
    {
       
        [TestMethod]
        public void Main()
        {
          
        }

        public bool HasCycle(ListNode head)
        {
            if (head == null) return false;

            ListNode slow = head;
            ListNode fast = head.next;

            while (slow != fast)
            {
                if (fast == null || fast.next == null)
                {
                    return false;
                }

                slow = slow.next;
                fast = fast.next.next;
            }

            return true;

        }

    }
}
