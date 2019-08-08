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
}
