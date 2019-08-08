using System;

namespace _190_linked_list
{
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
}