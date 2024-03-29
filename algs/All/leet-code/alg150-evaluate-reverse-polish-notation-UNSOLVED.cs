using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

// https://leetcode.com/problems/evaluate-reverse-polish-notation/

namespace evaluatereversepolishnotation
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            var tokens = new[] { "2", "1", "+", "3", "*" };
            var result = evalRpn(tokens);
            Console.WriteLine(result);
        }

        private static int evalRpn(string[] tokens)
        {            
            string opperators = "+-*/";
            Stack<string> stack = new Stack<string>();

            foreach (var token in tokens)
            {
                if (!opperators.Contains(token))
                {
                    stack.Push(token);
                }
                else
                {
                    int a = int.Parse(stack.Pop());
                    int b = int.Parse(stack.Pop());                    
                    int pairResult = token switch
                    {
                        "*" => a*b,
                        "/" => a/b,
                        "+" => a+b,
                        "-" => a-b,
                         _  => throw new NotImplementedException(),
                    };
                    stack.Push(pairResult.ToString());
                }
            }

            return int.Parse(stack.Pop());
        }
    }
}
