using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace QueueWithTwoStacks
{
    [TestClass]
    public class Run
    {
        [TestMethod]
        public void Main()
        {
            Console.WriteLine("Hello World!");

            MyQueue<int> myQueue = new MyQueue<int>();
            myQueue.enqueue(1);
            myQueue.enqueue(2);
            myQueue.enqueue(3);
            myQueue.enqueue(4);
            myQueue.enqueue(5);

            var res = myQueue.peek();
            var res1 = myQueue.dequeue();
        }
    }

    public class MyQueue<T> where T : struct
    {
        private Stack<T> stackNewestOnTop = new Stack<T>();
        private Stack<T> stackOldesOnTop = new Stack<T>();

        public void enqueue(T value)
        {
            stackNewestOnTop.Push(value);
        }

        public T peek()
        {
            // move elements from stackNewest to stackOldest
            shifthStacks();
            return stackOldesOnTop.Peek();
            // move elements back -- What if we didn't?
        }

        public T dequeue() // get 'oldest' item and remove it
        {
            shifthStacks();
            return stackOldesOnTop.Pop();
        }

        private void shifthStacks()
        {
            if (stackOldesOnTop.Any()) return;
            while (stackNewestOnTop.Any())
            {
                stackOldesOnTop.Push(stackNewestOnTop.Pop());
            }
        }
    }
}
