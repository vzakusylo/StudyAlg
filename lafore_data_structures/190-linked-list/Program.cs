using System;

namespace _190_linked_list
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkList list = new LinkList();
            list.InsertFirst(22, 2.99);
            list.InsertFirst(44, 44.99);
            list.InsertFirst(66, 66.99);
            list.InsertFirst(88, 88.99);

            list.DisplayList();

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
            Console.Write($"{'{'}{iData}, {dData}{'}'} ");
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
