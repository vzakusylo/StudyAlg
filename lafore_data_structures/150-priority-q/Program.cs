using System;

namespace _150_priority_q
{
    class Program
    {
        static void Main(string[] args)
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
}
