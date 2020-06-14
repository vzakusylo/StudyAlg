using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace drawing_book
{
    //https://www.hackerrank.com/challenges/drawing-book/problem?utm_campaign=challenge-recommendation&utm_medium=email&utm_source=24-hour-campaign

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            int result = pageCount(7, 3);
            Assert.AreEqual(1, result);

            result = pageCount(5, 4);
            Assert.AreEqual(0, result);
        }

        static int pageCount(int n, int p)
        {
         //   var size = n / 2 + 1;
         //   int[,] fromBegining = new int[size,2];

         //   var lowHight = 0;
         //for (int i = 0; i < size; i++)
         //   {
         //       for (int j = 0; j < 2; j++)
         //       {
         //           fromBegining[i, j] = lowHight++;
         //       }
         //   }

            var pageFromBegining = 0;
            var pageFromEnd = 0;
            if (p == 1)
            { return 0; }
            for (int i = 0; i < n; i++)
            {
                if (i < p && i % 2 == 1)
                {
                    pageFromBegining++;
                }
            }
            for (int i = n; i > 0; i--)
            {
                if (i > p && i % 2 == 0)
                {
                    pageFromEnd++;
                }
            }
            return Math.Min(pageFromBegining, pageFromEnd);

        }
    }
}
