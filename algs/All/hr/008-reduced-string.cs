using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace reduced_string
{
    //https://www.hackerrank.com/challenges/reduced-string/problem?utm_campaign=challenge-recommendation&utm_medium=email&utm_source=7-day-campaign

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var result = superReducedString("aa");
            Assert.AreEqual(string.Empty, result);

            result = superReducedString("aaabccddd");
            Assert.AreEqual("abd", result);

            result = superReducedString("baab");
            Assert.AreEqual(string.Empty, result);
        }
        
        static string superReducedString(string s)
        {
            var result = s;
            foreach (var c in s.ToArray())
            {
                char[] cs = { c,c };
                string strToReplace = new string(cs);
                result = result.Replace(strToReplace, string.Empty);
            }
            return result;
        }

    }
}
