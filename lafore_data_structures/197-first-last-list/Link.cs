using System;

namespace _197_first_last_list
{
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