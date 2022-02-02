using System;
using System.Linq;
using insertanodeataspecificposition;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace printtheelementsofalinkedlistinreverse1
{
    // https://www.hackerrank.com/challenges/print-the-elements-of-a-linked-list-in-reverse/problem

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
           
        }

        public static void reversePrint(SinglyLinkedListNode head)
        {
            // Write your code here
            SinglyLinkedListNode pre = null;
            SinglyLinkedListNode current = head;
            SinglyLinkedListNode post = head.next;

            while (post != null)
            {
                current.next = pre;
                pre = current;
                current = post;

                post = post.next;
            }

            current.next = pre;
            SinglyLinkedListNode newNode = current;
            while (newNode != null)
            {
                Console.WriteLine(newNode.data);
                newNode = newNode.next;
            }

        }
    }
}
