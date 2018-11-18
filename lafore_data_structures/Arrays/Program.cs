using System;

namespace Arrays
{
    class Program
    {
        static void Main(string[] args)
        {
            int maxSize = 100;
            OrdArray arr = new OrdArray(maxSize);

            arr.insert(77);
            arr.insert(99);
            arr.insert(55);

            var res = arr.find(55); 
        }
    }
}