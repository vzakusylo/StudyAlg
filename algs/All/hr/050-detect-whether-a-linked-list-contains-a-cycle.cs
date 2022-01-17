using System;
using System.Linq;
using insertanodeataspecificposition;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace detectwhetheralinkedlistcontainsacycle
{
    // https://www.hackerrank.com/challenges/detect-whether-a-linked-list-contains-a-cycle

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
           
        }

        static bool hasCycle(SinglyLinkedListNode head)
        {
            if (head == null)
            {
                return false;
            }

            SinglyLinkedListNode slow = head;
            SinglyLinkedListNode fast = head.next;

            while (fast != null && fast.next != null)
            {
                if (slow == fast)
                {
                    return true;
                }
                slow = slow.next;
                fast = fast.next.next;
            }

            return false;

        }

    }
}
