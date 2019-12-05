using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _133_brackets
{
    [TestClass]
    public class Template
    {
        [TestMethod]
        public void Main()
        {            
            var input = "Enter string containing delimiters:";
            BracketChecker theChecker = new BracketChecker(input);
            theChecker.Check();
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


    public class BracketChecker
    {
        private readonly string _input;

        public BracketChecker(string str)
        {
            _input = str;
        }

        public void Check()
        {
            var stackSize = _input.Length;
            var theStack = new StackX(stackSize);

            for (var i = 0; i < _input.Length; i++)
            {
                var ch = _input[i];
                switch (ch)
                {
                    case '{':
                    case '[':
                    case '(':
                        theStack.push(ch);
                        break;
                    case '}':
                    case ']':
                    case ')':
                        if (!theStack.isEmpty())
                        {
                            var chx = theStack.pop();
                            if (ch == '}' && chx != '{' ||
                                ch == ']' && chx != '[' ||
                                ch == ')' && chx != '(')
                                Console.WriteLine($"Error {ch} at {i}");
                        }
                        else
                        {
                            Console.WriteLine($"Error {ch} at {i}");
                        }

                        break;
                }
            }

            if (!theStack.isEmpty()) Console.WriteLine("Missing right delimiter");
        }
    }
}
