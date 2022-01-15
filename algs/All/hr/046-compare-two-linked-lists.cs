using insertanodeataspecificposition;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace comparetwolinkedlists
{
    // https://www.hackerrank.com/challenges/compare-two-linked-lists

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var head = new SinglyLinkedListNode(3);
            head.next = new SinglyLinkedListNode(2);
            head.next.next = new SinglyLinkedListNode(1);
            head.next.next.next = new SinglyLinkedListNode(4);

            var head2 = new SinglyLinkedListNode(3);
            head2.next = new SinglyLinkedListNode(2);
            head2.next.next = new SinglyLinkedListNode(1);
            head2.next.next.next = new SinglyLinkedListNode(4);

            var res = compareLists(head, head2);
            Assert.AreEqual(true, res);

        }


        static bool compareLists(SinglyLinkedListNode head1, SinglyLinkedListNode head2)
        {
            if (head1 == null && head2 == null) return true;

            SinglyLinkedListNode firstRunner = head1;
            SinglyLinkedListNode secondRunner = head2;
            while (firstRunner != null && secondRunner != null)
            {

                if (firstRunner.data != secondRunner.data)
                {
                    break;
                }

                if (firstRunner.next == null && secondRunner.next == null)
                {
                    return true;
                }

                firstRunner = firstRunner.next;
                secondRunner = secondRunner.next;
            }

            return false;


        }

        static bool compareListsR(SinglyLinkedListNode head1, SinglyLinkedListNode head2)
        {
            if (head1 == null && head2 == null) return true;
            if (head1 == null && head2 != null) return false;
            if (head1 != null && head2 == null) return false;
            if (head1.data != head2.data) return false;

            return compareListsR(head1.next, head2.next);
        }


    }
}
