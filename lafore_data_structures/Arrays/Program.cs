using System;

namespace Arrays
{
    class Program
    {
        static void Main(string[] args)
        {
            int maxSize = 8;
            ArraySel arr = new ArraySel(maxSize);
            arr.insert(77);
            arr.insert(99);
            arr.insert(44);
            arr.insert(55);
            arr.insert(22);
            arr.insert(88);
            
            arr.selectionSort();

            // OrderedArrayDemo();
        }

        private static void OrderedArrayDemo()
        {
            int maxSize = 8;
            OrdArray arr = new OrdArray(maxSize);

            arr.insert(77);
            arr.insert(99);
            arr.insert(44);
            arr.insert(55);
            arr.insert(22);
            arr.insert(88);

            var res = arr.find(55);
            res = arr.find(99);

            arr.delete(00);
            arr.delete(55);
        }
    }

    class ArraySel
    {
        private long[] a;
        private int nElem;
        
        public ArraySel(int max)
        {
            a = new  long [max];
            nElem = 0;
        }

        public void insert(long value)
        {
            a[nElem] = value;
            nElem++;
        }

        public void selectionSort()
        {
            int aut, inn, min;
            for (aut = 0; aut < nElem-1; aut++)
            {
                min = aut;
                for (inn = aut+1; inn < nElem; inn++)
                {
                    if (a[inn] < a[min])
                    {
                        min = inn;
                    }
                }
                swap(aut, min);
            }
        }

        private void swap(int aut, int min)
        {
            long temp = a[aut];
            a[aut] = a[min];
            a[min] = temp;
        }
    }
    
    
}