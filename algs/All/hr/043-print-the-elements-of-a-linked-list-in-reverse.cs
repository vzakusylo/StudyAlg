using System;
using System.Collections.Generic;
using System.Linq;
using insertanodeataspecificposition;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace printtheelementsofalinkedlistinreverse
{
    // https://www.hackerrank.com/challenges/print-the-elements-of-a-linked-list-in-reverse

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var head = new SinglyLinkedListNode(3);
            head.next = new SinglyLinkedListNode(2);
            head.next.next = new SinglyLinkedListNode(1);

            reversePrint(head);
        }

        public static void reversePrint(SinglyLinkedListNode head)
        {
            if (head == null) return;

            var stack = new Stack<SinglyLinkedListNode>();
            var current = head;
            while (current != null)
            {
                stack.Push(current);
                current = current.next;
            }

            while (stack.Any())
            {
                var item = stack.Pop();
                Console.WriteLine(item.data);
            }
        }
    }
}
