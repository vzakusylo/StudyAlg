using System;

namespace _197_first_last_list
{
    public class FirstLastList
    {
        private Link first;
        private Link last;

        public FirstLastList()
        {
        }

        public bool IsEmpty()
        {
            return first == null;
        }

        public void InsertFirst(long dd)
        {
            Link newLink = new Link(dd);
            if (IsEmpty())
            {
                last = newLink;
            }

            newLink.next = first;
            first = newLink;
        }

        public void InsertLast(long dd)
        {
            Link newLink = new Link(dd);
            if (IsEmpty())
            {
                first = newLink;
            }
            else
            {
                last.next = newLink;
            }

            last = newLink;
        }

        public long DeleteFirst()
        {
            long temp = first.dData;
            if (first.next == null)
            {
                last = null;
            }

            first = first.next;
            return temp;
        }

        public void DisplayList()
        {
            Console.Write("List (first->last): ");
            Link current = first;
            while (current != null)
            {
                current.displayLink();
                current = current.next;
            }
            Console.WriteLine();
        }
    }
}