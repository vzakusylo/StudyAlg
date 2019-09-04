using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Page190
{
    [TestClass]
    public class ListStack
    {
        [TestMethod]
        public void Main()
        {
            LinkList list = new LinkList();
            list.InsertFirst(22, 2.99);
            list.InsertFirst(44, 44.99);
            list.InsertFirst(66, 66.99);
            list.InsertFirst(88, 88.99);

            list.DisplayList();

            Console.WriteLine();
            Link f = list.find(44);
            Console.WriteLine(f != null ? $"Found link with key {f.iData}" : "Can't find link");

            Link d = list.delete(66);
            Console.WriteLine(d != null ? $"Deleted link with key {d.iData}" : "Can't delete link");

            while (!list.IsEmpty())
            {
                Link link = list.DeleteFirst();
                Console.Write("Deleted: ");
                link.DisplayLink();
                Console.WriteLine();
            }

            list.DisplayList();
        }
    }

    public class Link
    {
        public int iData;
        public double dData;
        public Link next;

        public Link(int id, double dd)
        {
            iData = id;
            dData = dd;
        }

        public void DisplayLink()
        {
            Console.Write(ToString());
        }

        public override string ToString()
        {
            return $"{'{'}{iData}, {dData}{'}'} ";
        }
    }

    public class LinkList
    {
        private Link _first;

        public LinkList()
        {
            _first = null;
        }

        public bool IsEmpty()
        {
            return _first == null;
        }

        public Link find(int key)
        {
            Link current = _first;
            while (current.iData != key)
            {
                if (current.next == null)
                {
                    return null;
                }
                else
                {
                    current = current.next;
                }
            }
            return current;
        }


        public Link delete(int key)
        {
            Link current = _first;
            Link previous = _first;

            while (current.iData != key)
            {
                if (current.next == null)
                {
                    return null;
                }
                else
                {
                    previous = current;
                    current = current.next;
                }
            }

            if (current == _first)
            {
                _first = _first.next;
            }
            else
            {
                previous.next = current.next;
            }
            return current;
        }

        public void InsertFirst(int id, double dd)
        {
            Link newLink = new Link(id, dd);
            newLink.next = _first;
            _first = newLink;
        }

        public Link DeleteFirst()
        {
            Link temp = _first;
            _first = _first.next;
            return temp;
        }

        public void DisplayList()
        {
            Console.Write("List (first-->last): ");
            Link current = _first;
            while (current != null)
            {
                current.DisplayLink();
                current = current.next;
            }
        }
    }
}
