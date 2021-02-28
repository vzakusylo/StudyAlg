using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace funny_str
{
    // https://www.hackerrank.com/challenges/funny-string

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var result = funnyString("acxz");
            Assert.AreEqual("Funny", result);

            result = funnyString("bcxz");
            Assert.AreEqual("Not Funny", result);
        }

    static string funnyString(string s)
    {
        var direct = s.ToArray();
        var forward = s.Reverse().ToArray();

        var directAbsResult = new int[direct.Length-1];
        var forwardAbsResult = new int[forward.Length-1];

        for (int i = 0; i < direct.Length-1; i++)
        {
            directAbsResult[i] = Math.Abs((int)direct[i] - (int)direct[i + 1]);
        }
        for (int i = 0; i < forward.Length - 1; i++)
        {
            forwardAbsResult[i] = Math.Abs((int)forward[i] - (int)forward[i + 1]);
        }

        for (int i = 0; i < directAbsResult.Length; i++)
        {
            if (directAbsResult[i] != forwardAbsResult[i])
            {
                return "Not Funny";
            }
        }

        return "Funny";

    }
}
}
