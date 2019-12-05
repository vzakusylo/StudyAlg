using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _212_sorted_list
{
    [TestClass]
    public class Run
    {
        [TestMethod]
        public void Main()
        {
            SortedList list = new SortedList();
            list.Insert(20);
            list.Insert(30);
            list.DisplayList();

            list.Insert(10);
            list.Insert(30);
            list.Insert(50);
            list.DisplayList();

            list.Remove();
            list.DisplayList();
        }
    }

    public class SortedList
    {
        private Link<long> first;

        public SortedList()
        {
            first = null;
        }

        public bool IsEmpty()
        {
            return (first == null);
        }

        public void Insert(long key)
        {
            Link<long> newLink = new Link<long>(key);
            Link<long> previous = null;
            Link<long> current = first;

            while (current != null && current.Data < key)
            {
                previous = current;
                current = current.Next;
            }

            if (previous == null)
            {
                first = newLink;
            }
            else
            {
                previous.Next = newLink;
            }
            newLink.Next = current;
        }

        public Link<long> Remove()
        {
            Link<long> temp = first;
            first = first.Next;
            return temp;
        }

        public void DisplayList()
        {
            Console.WriteLine("List (first-->last):");
            Link<long> current = first;
            while (current != null)
            {
                current.DisplayLink();
                current = current.Next;
            }
            Console.WriteLine();
        }
    }

    public class Link<T> where T : IComparable
    {
        public T Data { get; private set; }
        public Link<T> Next { get; set; }

        public Link(T data)
        {
            Data = data;
        }

        public void DisplayLink()
        {
            Console.WriteLine($"{Data.ToString()}");
        }
    }

}
