using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinaryHeap2
{

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            Random r = new Random(100);
            int n = r.Next(100) + 1;
            BinaryHeap bh = new BinaryHeap(n);
            Enumerable.Range(0, n).ToList().ForEach(x => bh.Add(r.Next(100)));           
        }
    }

    internal class BinaryHeap
    {
        private int size;
        int[] heap;

        public BinaryHeap(int n)
        {
            heap = new int[n];
        }

        internal void Add(int value)
        {
            heap[size] = value;
            Up(size++);
        }

        private void Up(int position)
        {
            while (position > 0 )
            {
                int parent = (position - 1) / 2;
                if (heap[position] >= heap[parent])
                {
                    break;
                }
                Swap(position, parent);
                position = parent;
            }
        }

        private void Swap(int position, int parent)
        {
            var temp = heap[position];
            heap[position] = heap[parent];
            heap[parent] = temp;
        }
    }
}
