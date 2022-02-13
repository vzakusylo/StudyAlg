using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace beautiful_binary_string
{
    // https://www.hackerrank.com/challenges/beautiful-binary-string/problem?h_r=internal-search&isFullScreen=true

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var str = "0100101010";
            var res = beautifulBinaryString(str);

            Assert.AreEqual(3, res);
        }

        public static int beautifulBinaryString(string b)
        {
            // 0100101010
            // 0110101010
            // 0110111010
            // 0110111110
            // 010 marker

            int numOfChanges = 0;
            char[] bArray = b.ToCharArray();

            for (int i = 1; i < bArray.Length - 1; i++)
            {
                if (bArray[i - 1] == '0' && bArray[i] == '1' && bArray[i + 1] == '0')
                {
                    bArray[i + 1] = '1';
                    numOfChanges++;
                }
            }
            return numOfChanges;
        }


    }
}
