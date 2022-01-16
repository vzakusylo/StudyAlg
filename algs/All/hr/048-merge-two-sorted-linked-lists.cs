using System;
using System.Linq;
using insertanodeataspecificposition;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mergetwosortedlinkedlists
{
    // https://www.hackerrank.com/challenges/merge-two-sorted-linked-lists

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
           
        }

        static SinglyLinkedListNode mergeLists(SinglyLinkedListNode head1, SinglyLinkedListNode head2)
        {
            if (head1 == null) return head2;
            if (head2 == null) return head1;

            SinglyLinkedListNode head3 = null;

            if (head1.data < head2.data)
            {
                head3 = head1;
                head1 = head1.next;
            }
            else
            {
                head3 = head2;
                head2 = head2.next;
            }
            SinglyLinkedListNode currentNode = head3;
            while (head1 != null && head2 != null)
            {
                if (head1.data < head2.data)
                {
                    currentNode.next = head1;
                    head1 = head1.next;
                }
                else
                {
                    currentNode.next = head2;
                    head2 = head2.next;
                }
                currentNode = currentNode.next;
            }
            if (head1 == null)
            {
                currentNode.next = head2;
            }
            else
            {
                currentNode.next = head1;
            }

            return head3;
        }

    }
}
