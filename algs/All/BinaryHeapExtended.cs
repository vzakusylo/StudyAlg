using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinaryHeapExtended
{
    [TestClass]
    public class Run
    {
        [TestMethod]
        public void Main()
        {

            BinaryHeapExtended heap = new BinaryHeapExtended(10);
            heap.Add(0, 4);
            heap.Add(1, 5);
            heap.Add(2, 2);

            heap.ChangePriority(1, 3);
            heap.ChangePriority(2, 6);
            heap.Remove(0);

            while (heap.Size != 0)
            {
                Console.WriteLine($"{heap.heap[0]} {heap.Remove()}");
            }
        }
    }

    public class BinaryHeapExtended
    {
        public long[] heap;
        private int[] pos2Id;
        private int[] id2Pos;
        private int size;

        public int Size => size;


        public BinaryHeapExtended(int n)
        {
            heap = new long[n];
            pos2Id = new int[n];
            id2Pos = new int[n];
        }

        public int Remove()
        {
            int removeId = pos2Id[0];
            heap[0] = heap[size--];
            pos2Id[0] = pos2Id[size];
            id2Pos[pos2Id[0]] = 0;
            Down(0);
            return removeId;
        }

        public void Remove(int id)
        {
            int pos = id2Pos[id];
            pos2Id[pos] = pos2Id[--size];
            id2Pos[pos2Id[pos]] = pos;
            ChangePriority(pos2Id[pos], heap[size]);
        }

        public void Add(int id, long value)
        {
            heap[size] = value;
            pos2Id[size] = id;
            id2Pos[id] = size;
            Up(size++);
        }

        public void ChangePriority(int id, long value)
        {
            int pos = id2Pos[id];
            if (heap[pos] < value)
            {
                heap[pos] = value;
                Down(pos);
            }
            else if (heap[pos] > value)
            {
                heap[pos] = value;
                Up(pos);
            }
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

                if (child + 1 < size && heap[child + 1] < heap[child])
                {
                    ++child;
                }

                if (heap[pos] <= heap[child])
                {
                    break;
                }

                Swap(pos, child);
                pos = child;
            }
        }

        private void Swap(int i, int j)
        {
            long tt = heap[i];
            heap[i] = heap[j];
            heap[j] = tt;
            int t = pos2Id[i];
            pos2Id[i] = pos2Id[j];
            pos2Id[j] = t;
            id2Pos[pos2Id[i]] = i;
            id2Pos[pos2Id[j]] = j;
        }
    }

}
