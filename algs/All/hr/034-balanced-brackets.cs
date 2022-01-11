using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace balancedBrackets
{
    // https://www.hackerrank.com/challenges/balanced-brackets

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var str = "{[()]}";
            var result = isBalanced(str);
            Assert.AreEqual("YES", result);

            str = "{[(])}";
            result = isBalanced(str);
            Assert.AreEqual("NO", result);

            str = "{{[[(())]]}}";
            result = isBalanced(str);
            Assert.AreEqual("YES", result);
        }

        public static String isBalanced(String s)
        {
            Stack<char> stack = new Stack<char>();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(' || s[i] == '{' || s[i] == '[')
                {
                    stack.Push(s[i]);
                }
                else
                {
                    if (stack.Count == 0)
                    {
                        return "NO";
                    }
                    else
                    {
                        var popVal = stack.Pop();
                        if (s[i] == ')' && popVal != '(')
                        {
                            return "NO";
                        }
                        if (s[i] == '}' && popVal != '{')
                        {
                            return "NO";
                        }
                        if (s[i] == ']' && popVal != '[')
                        {
                            return "NO";
                        }
                    }
                }
            }

            if (stack.Count == 0)
            {
                return "YES";
            }
            else
            {
                return "NO";
            }
        }

        public static String isBalanced1(String s)
        {
            Stack<char> stack = new Stack<char>();
            var openChars = new[] {'{', '[', '('};
            var closeChars = new[] {'}', ']', ')'};
            var pairs = new Dictionary<char, char>();
            pairs.Add('{', '}');
            pairs.Add('[', ']');
            pairs.Add('(', ')');

            for (int i = 0; i < s.Length; i++)
            {
                var curChar = s[i];
                if (openChars.Contains(curChar))
                {
                    stack.Push(curChar);
                }

                if (closeChars.Contains(curChar))
                {
                    var openChar = stack.Pop();
                    if (pairs.ContainsKey(openChar))
                    {
                        var expectedClosing = pairs[openChar];
                        if (expectedClosing != curChar)
                        {
                            return "NO";
                        }
                    }
                }
            }
            return "YES";
        }
    }
}
