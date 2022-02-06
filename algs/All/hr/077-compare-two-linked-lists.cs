using System;
using System.Linq;
using insertanodeataspecificposition;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace comparetwolinkedlists1
{
    // https://www.hackerrank.com/challenges/compare-two-linked-lists/problem?h_r=internal-search

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
           
        }

        static bool CompareLists(SinglyLinkedListNode head1, SinglyLinkedListNode head2)
        {
            while (head1 != null && head2 != null)
            {
                if (head1.data != head2.data)
                {
                    return false;
                }

                head1 = head1.next;
                head2 = head2.next;
            }

            return head1 == head2 ? true : false;

        }

    }
}
