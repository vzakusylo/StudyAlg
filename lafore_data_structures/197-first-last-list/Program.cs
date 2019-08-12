using System;

namespace _197_first_last_list
{
    class Program
    {
        static void Main(string[] args)
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
}
