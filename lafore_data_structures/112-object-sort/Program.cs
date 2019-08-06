using System;

namespace _112_object_sort
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrayInObj arr = new ArrayInObj(100);
            arr.Insert("Evans", "Patty", 24);
            arr.Insert("Smith", "Lorraine", 37);
            arr.Insert("Yee", "Tom", 43);
            arr.Insert("Adams", "Henry", 63);
            arr.Insert("Hashimoto", "Sato", 21);
            arr.Insert("Stimson", "Henry", 21);
              
            Console.WriteLine("Before sorting:");
            arr.Display();
            arr.InsertionSort();

            Console.WriteLine("After sorting");
            arr.Display();
        }
    }
}
