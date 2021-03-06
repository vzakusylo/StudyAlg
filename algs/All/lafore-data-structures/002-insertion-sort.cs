using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _002_insertion_sort
{
    [TestClass]
    public class Template
    {
        [TestMethod]
        public void Main()
        {
            const int maxSize = 100;
            var arr = new ArrayInst(maxSize);

            arr.Insert(7);
            arr.Insert(4);
            arr.Insert(3);
            arr.Insert(5);
            arr.Insert(6);
            arr.Insert(2);
            //arr.Insert(11);
            //arr.Insert(00);
            //arr.Insert(33);

            arr.Display();

            arr.InsertionSort();

            arr.Display();
        }
    }

    class ArrayInst
    {
        private long[] a;
        private int nElems;

        public ArrayInst(int max)
        {
            a = new long[max];
            nElems = 0;
        }

        public void Insert(long value)
        {
            a[nElems] = value;
            nElems++;
        }

        public void Display()
        {
            for (int j = 0; j < nElems; j++)
            {
                Console.Write($"{a[j]} ");
            }
            Console.WriteLine();
        }

        public void InsertionSort()
        {
            int inn, outt;
            for (outt = 1; outt < nElems; outt++)
            {
                long temp = a[outt];
                inn = outt;
                long aprev = a[inn - 1];
                while (inn > 0 && aprev >= temp)
                {
                    a[inn] = a[inn - 1];
                    --inn;
                }
                a[inn] = temp;
            }
        }
    }

}
