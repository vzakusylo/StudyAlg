using System;

namespace _133_brackets
{
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