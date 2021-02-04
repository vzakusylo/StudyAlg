using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace hackerland_radio_transmitters
{
    //https://www.hackerrank.com/challenges/hackerland-radio-transmitters

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var res = hackerlandRadioTransmitters(new[] { 1, 2, 3, 4, 5 }, 1);
            Assert.AreEqual(2, res);

            res = hackerlandRadioTransmitters(new[] { 7, 2, 4, 6, 5, 9, 12, 11 }, 2);
            Assert.AreEqual(3, res);

            res = hackerlandRadioTransmitters(new[] { 9, 5, 4, 2, 6, 15, 12 }, 2);
            Assert.AreEqual(4, res);

            //   0   0 0 0     0       0        0
            // 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15
        }

       
        static int hackerlandRadioTransmitters(int[] x, int k)
        {
            var maxCoverage = k * 2 + 1; // 5
            var minElem = x.Min(); // 2
            var maxElem = x.Max() + 1; // 12

            var numOfTransm = 1;
            var count = 0;
            for (int i = minElem; i <= maxElem; i++)
            {                
                if (count < maxCoverage) // 2 < 5 * 1
                {
                    count++;
                }
                else
                {
                    count = 0;
                    numOfTransm++;
                }
            }

            return numOfTransm;
        }
    }
}
