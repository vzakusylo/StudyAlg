using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace caesarcipher2
{
    //https://www.hackerrank.com/challenges/camelcase/problem?utm_campaign=challenge-recommendation&utm_medium=email&utm_source=7-day-campaign&h_r=next-challenge&h_v=zen

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var result = caesarCipher("middle-Outz", 2);
            Assert.AreEqual("okffng-Qwvb", result);

            result = caesarCipher("Always-Look-on-the-Bright-Side-of-Life", 5);
            Assert.AreEqual("Fqbfdx-Qttp-ts-ymj-Gwnlmy-Xnij-tk-Qnkj", result);

            result = caesarCipher("www.abc.xy", 87);
            Assert.AreEqual("fff.jkl.gh", result);

            result = caesarCipher("159357lcfd", 98);
            Assert.AreEqual("159357fwzx", result);
        }

        static string caesarCipher(string s, int k)
        {
            string result = string.Empty;            
            foreach (var c in s.ToArray())
            {
                bool isUpper = false;
                if (char.IsUpper(c))
                {
                    isUpper = true;
                }               
                
                var charAsInt = (int)char.ToLower(c);
                if (charAsInt >= 97 && charAsInt <= 122)
                {
                    charAsInt = charAsInt + k;
                    while (charAsInt > 122)
                    {
                        charAsInt = charAsInt % 122 + 96;
                    }
                }
                char shiftedChar = (char)charAsInt;
                result += isUpper ? char.ToUpper(shiftedChar) : shiftedChar;
            }
            return result;
        }

    }
}
