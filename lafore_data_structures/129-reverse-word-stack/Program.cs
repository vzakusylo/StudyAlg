using System;
using System.Globalization;

namespace _129_reverse_word_stack
{
    class Program
    {
        static void Main(string[] args)
        {
            string input, output;
            while (true)
            {
                Console.WriteLine("Please enter a string");
                input = Console.ReadLine();
                if (input == string.Empty)
                {
                    break;
                }

                Reverser theReverser = new Reverser(input);
                output = theReverser.DoReverse();
                Console.WriteLine(output);
            }
        }
    }

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

        public bool isEmpty()
        {
            return top == -1;
        }
    }

    public class Reverser
    {
        private string intput;
        private string output;

        public Reverser(string s)
        {
            intput = s;
        }

        public string DoReverse()
        {
            int stackSize = intput.Length;
            StackX stack = new StackX(stackSize);

            for (int i = 0; i < intput.Length; i++)
            {
                char ch = intput[i];
                stack.push(ch);
            }

            string output = string.Empty;

            while (!stack.isEmpty())
            {
                char ch = stack.pop();
                output = output + ch;
            }

            return output;
        }
    }   
}
