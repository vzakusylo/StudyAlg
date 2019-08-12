using System;
using System.Collections.Generic;
using System.Linq;

namespace BalancedParenthesesInExpression
{
    class Program
    {
        public static char[][] Tokens = {new[] {'{', '}'}, new[] {'[', ']'}, new[] {'(', ')'}};

        

        public static bool IsBalanced(String expession)
        {
            Stack<char> stack = new Stack<char>();
            foreach (var c in expession.ToCharArray())
            {
                if (IsOpenTerm(c))
                {
                    stack.Push(c);
                }
                else
                {
                    if (!stack.Any() || !Matches(stack.Pop(), c))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static bool Matches(char openTerm, char closeTerm)
        {
            foreach (var token in Tokens)
            {
                if (token[0] == openTerm)
                {
                    return token[1] == closeTerm;
                }
            }
            return false;
        }

        private static bool IsOpenTerm(char c)
        {
            foreach (var token in Tokens)
            {
                if (token[0] == c)
                {
                    return true;
                }
            }

            return false;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
