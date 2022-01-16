using System;
using System.Linq;
using insertanodeataspecificposition;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace deleteduplicatevaluenodesfromasortedlinkedlist
{
    // https://www.hackerrank.com/challenges/delete-duplicate-value-nodes-from-a-sorted-linked-list

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
           
        }

        public static SinglyLinkedListNode removeDuplicates(SinglyLinkedListNode head)
        {
            // Write your code here
            if (head == null) return head;

            SinglyLinkedListNode newHead = head;

            while (head.next != null)
            {
                if (head.data == head.next.data)
                {
                    head.next = head.next.next;
                }
                else
                {
                    head = head.next;
                }
            }

            return newHead;
        }


    }
}
