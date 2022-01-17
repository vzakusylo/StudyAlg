using System;
using System.Linq;
using insertanodeataspecificposition;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace findthemergepointoftwojoinedlinkedlists
{
    // https://www.hackerrank.com/challenges/find-the-merge-point-of-two-joined-linked-lists

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
           
        }

        static int findMergeNode(SinglyLinkedListNode head1, SinglyLinkedListNode head2)
        {
            SinglyLinkedListNode head1Current = head1;
            SinglyLinkedListNode head2Current = head2;

            while (head1Current != head2Current)
            {
                if (head1Current == null)
                {
                    head1Current = head2;
                }
                else
                {
                    head1Current = head1Current.next;
                }

                if (head2Current == null)
                {
                    head2Current = head1;
                }
                else
                {
                    head2Current = head2Current.next;
                }

            }

            return head1Current.data;

        }


    }
}
