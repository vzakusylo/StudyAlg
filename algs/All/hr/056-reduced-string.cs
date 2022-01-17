using System;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace template
{
    //https://www.hackerrank.com/challenges/common-child

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var s = "aaabccddd";
            var res = superReducedString(s);
            Assert.AreEqual("abd", res);
        }

        public static string superReducedString(string s)
        {
            StringBuilder sb = new StringBuilder();
            int index = 0;
            while (index < s.Length)
            {
                if (sb.Length == 0)
                {
                    sb.Append(s[index]);
                    index++;
                    continue;
                }
                 // if (sb.Length > 0 && sb[sb.Length - 1] == s[index])
                    if (sb.Length > 0 && sb[^1] == s[index])
                {
                    sb.Remove(sb.Length - 1, 1);
                    index++;
                    continue;
                }

                sb.Append(s[index]);
                index++;
            }
            if (sb.Length == 0)
            {
                return "Empty String";
            }
            else
            {
                return sb.ToString();
            }
        }


    }
}
