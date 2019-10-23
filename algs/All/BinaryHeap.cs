using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace All
{

    [TestClass]
    public class Main
    {
        [TestMethod]
        public void EntryPoint()
        {
            Random r = new Random();
            BinaryHeap h = new BinaryHeap(1000);
            for (int step = 0; step < 1000; step++)
            {
                int n = r.Next(1000) + 1;
                h.Add(n);
            }
            for (int step = 0; step < 1000; step++)
            {
                h.RemoveMin();
            }
        }
    }

    public class BinaryHeap
    {
        private int[] heap;
        private int size;

        public BinaryHeap(int n)
        {
            heap = new int[n];
        }

        // build heap in O(n)
        public BinaryHeap(int[] values)
        {
            heap = (int[])values.Clone();
            size = values.Length;
            for (int i = size/2-1; i >= 0; i--)
            {
                Down(i);
            }
        }

        public void Add(int value)
        {
            heap[size] = value;
            Up(size++);
        }

        public int RemoveMin()
        {
            int removed = heap[0];
            heap[0] = heap[--size];
            Down(0);
            return removed;
        }

        public void Up(int pos)
        {
            while (pos > 0)
            {
                int parent = (pos - 1) / 2;
                if (heap[pos] >= heap[parent])
                {
                    break;
                }
                Swap(pos, parent);
                pos = parent;
            }
        }

        public void Down(int pos)
        {
            while (true)
            {
                int child = 2 * pos + 1;
                if (child >= size)
                {
                    break;     
                }

                if (child + 1 < size && heap[child +1] < heap[child])
                {
                    ++child;
                }

                if (heap[pos] <= heap[child])
                    break;
                Swap(pos, child);
                pos = child;
            }
        }

        public void Swap(int i, int j)
        {
            int t = heap[i];
            heap[j] = heap[i];
            heap[i] = t;
        }


    }
}
