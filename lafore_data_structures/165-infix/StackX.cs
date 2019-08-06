using System;

namespace _165_infix
{
    public class StackX
    {
        private int maxSize;
        private char[] stackArray;
        private int top;

        public StackX(int max)
        {
            maxSize = max;
            stackArray = new char[max];
            top = -1;
        }

        public void push(char j)
        {
            stackArray[++top] = j;
        }

        public char pop()
        {
            return stackArray[top--];
        }

        public char peek()
        {
            return stackArray[top];
        }

        public char peekN(int n) // read element with index N
        {
            return stackArray[n];
        }

        public bool isEmpty()
        {
            return top == -1;
        }

        public int size() // current stack size
        {
            return top + 1;
        }

        public void displayStack(string s)
        {
            Console.WriteLine(s);
            Console.WriteLine("Stack (bottom-->top):");
            for (int j = 0; j < size(); j++)
            {
                Console.WriteLine(peekN(j));
            }
        }
    }
}