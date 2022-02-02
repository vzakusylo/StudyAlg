using System;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace commonchild2
{
    //https://www.hackerrank.com/challenges/common-child

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
           
        }

        public static int alternatingCharacters(string s)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(s[0]);

            for (int i = 1; i < s.Count(); i++)
            {
                // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-8.0/ranges#:~:text=C%23%20has%20no%20way%20of,must%20be%20convertible%20to%20System.
                //if (sb[sb.Length - 1] == s[i]) continue;
                if (sb[^1] == s[i]) continue;

                sb.Append(s[i]);
            }

            return s.Length - sb.Length;
        }


    }
}
