using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Page201
{
    [TestClass]
    public class ListStack
    {
        [TestMethod]
        public void Main()
        {
            LinkStack theStack = new LinkStack();

            theStack.Push(20);
            theStack.Push(40);

            theStack.DisplayStack();

            theStack.Push(60);
            theStack.Push(80);

            theStack.DisplayStack();

            theStack.Pop();
            theStack.Pop();

            theStack.DisplayStack();
        }
    }

    public class LinkStack
    {
        private LinkList theList;

        public LinkStack()
        {
            theList = new LinkList();
        }

        public void Push(long j)
        {
            theList.InsertFirst(j);
        }

        public long Pop()
        {
            return theList.DeleteFirst();
        }

        public bool IsEmpty()
        {
            return theList.IsEmpty();
        }

        public void DisplayStack()
        {
            Console.Write("Stack (top-->bottom): ");
            theList.DisplayList();
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
            return (_first == null);
        }

        public void InsertFirst(long dd)
        {
            Link newLink = new Link(dd);
            newLink.Next = _first;
            _first = newLink;
        }

        public long DeleteFirst()
        {
            Link temp = _first;
            _first = _first.Next;
            return temp.DData;
        }

        public void DisplayList()
        {
            Link current = _first;
            while (current != null)
            {
                current.DisplayLink();
                current = current.Next;
            }

            Console.WriteLine("");
        }
    }

    public class Link
    {
        public long DData;
        public Link Next;

        public Link(long dd)
        {
            DData = dd;
        }

        public void DisplayLink()
        {
            Console.Write($"{DData} ");
        }
    }
}
