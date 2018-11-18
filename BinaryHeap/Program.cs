using System;

namespace BinaryHeap
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            BinaryHeap h = new BinaryHeap(1000);
            for (int op = 0; op < 1000; op++)
            {
                int v = rnd.Next(1,100);
                h.Add(v);
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

        public void Add(int value)
        {
            heap[size] = value;
            Up(size++);
        }

        private void Up(int pos)
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

        private void Swap(int pos, int parent)
        {
            int t = heap[pos];
            heap[pos] = heap[parent];
            heap[parent] = t;
        }
    }
}
