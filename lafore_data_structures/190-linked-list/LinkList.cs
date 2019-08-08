using System;

namespace _190_linked_list
{
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