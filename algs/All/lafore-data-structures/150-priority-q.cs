using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _150_priority_q
{
    [TestClass]
    public class Template
    {
        [TestMethod]
        public void Main()
        {
            PriorityQueue priorityQueue = new PriorityQueue(5);
            priorityQueue.Insert(30);
            priorityQueue.Insert(50);
            priorityQueue.Insert(10);
            priorityQueue.Insert(40);
            priorityQueue.Insert(20);

            while (!priorityQueue.isEmpty())
            {
                var item = priorityQueue.remove();
                Console.Write($"{item} ");
            }
        }
    }

    class PriorityQueue
    {
        private int maxSize;
        private long[] queueArray;
        private int nItems;

        public PriorityQueue(int s)
        {
            maxSize = s;
            queueArray = new long[s];
            nItems = 0;
        }

        public void Insert(long item)
        {
            int j = 0;
            if (nItems == 0)
            {
                queueArray[nItems++] = item;
            }
            else
            {
                for (j = nItems - 1; j >= 0; j--)
                {
                    if (item > queueArray[j])
                    {
                        queueArray[j + 1] = queueArray[j];
                    }
                    else
                    {
                        break;
                    }
                }
                queueArray[j + 1] = item;
                nItems++;
            }
        }

        public long remove()
        {
            {
                return queueArray[--nItems];
            }
        }

        public long peekMin()
        {
            return queueArray[nItems - 1];
        }

        public bool isEmpty()
        {
            return nItems == 0;
        }

        public bool isFull()
        {
            return (nItems == maxSize);
        }
    }
}
