using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _165_infix
{
    [TestClass]
    public class Template
    {
        [TestMethod]
        public void Main()
        {
            InToPos theTrans = new InToPos("A*(B+C)-D(E+F)");
            var output = theTrans.doTrans();
            Console.WriteLine($"Postfix is {output}");
        }
    }

    public class InToPos
    {
        private StackX theStack;
        private String input;
        private String outpup;

        public InToPos(String n)
        {
            input = n;
            int stackSize = n.Length;
            theStack = new StackX(stackSize);
        }

        public string doTrans()
        {
            for (int j = 0; j < input.Length; j++)
            {
                char ch = input[j];
                theStack.displayStack($"For {ch} ");
                switch (ch)
                {
                    case '+':
                    case '-':
                        gotOper(ch, 1);
                        break;

                    case '*':
                    case '/':
                        gotOper(ch, 2);
                        break;
                    case '(':
                        theStack.push(ch);
                        break;
                    case ')':
                        gotParen(ch);
                        break;
                    default:
                        outpup = outpup + ch;
                        break;
                }
            }

            while (!theStack.isEmpty())
            {
                theStack.displayStack("while");
                outpup = outpup + theStack.pop();
            }
            theStack.displayStack("End ");
            return outpup;
        }

        public void gotParen(char ch)
        {
            while (!theStack.isEmpty())
            {
                char chx = theStack.pop();
                if (chx == '(')
                {
                    break;
                }
                else
                {
                    outpup = outpup + chx;
                }
            }
        }

        public void gotOper(char opThis, int precl)
        {
            while (!theStack.isEmpty())
            {
                char opTop = theStack.pop();
                if (opTop == '(')
                {
                    theStack.push(opTop);
                    break;
                }
                else
                {
                    int prec2;
                    if (opTop == '+' || opTop == '-')
                    {
                        prec2 = 1;
                    }
                    else
                    {
                        prec2 = 2;
                    }

                    if (prec2 < precl)
                    {
                        theStack.push(opTop);
                        break;
                    }
                    else
                    {
                        outpup = outpup + opTop;
                    }
                }
                theStack.push(opThis);
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
