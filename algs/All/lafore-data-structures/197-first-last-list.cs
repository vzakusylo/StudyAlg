﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Page197
{
    [TestClass]
    public class ListStack
    {
        [TestMethod]
        public void Main()
        {
            FirstLastList theList = new FirstLastList();

            theList.InsertFirst(22);
            theList.InsertFirst(44);
            theList.InsertFirst(66);

            theList.InsertLast(11);
            theList.InsertLast(33);
            theList.InsertLast(55);

            theList.DisplayList();

            theList.DeleteFirst();
            theList.DeleteFirst();

            theList.DisplayList();
        }
    }

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


    public class Link
    {
        public long dData;
        public Link next;

        public Link(long d)
        {
            dData = d;
        }

        public void displayLink()
        {
            Console.Write($"{dData} ");
        }
    }
}
