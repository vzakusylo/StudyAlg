using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _129_reverse_word_stack
{
    [TestClass]
    public class Template
    {
        [TestMethod]
        public void Main()
        {
            string input, output;

            input = "Please enter a string";            

            Reverser theReverser = new Reverser(input);
            output = theReverser.DoReverse();
            Console.WriteLine(output);
            
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
}
