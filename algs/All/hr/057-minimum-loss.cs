using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace minimumloss
{
    //https://www.hackerrank.com/challenges/minimum-loss/problem?isFullScreen=true

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var prices = new long[] {20, 7, 8, 2, 5};
            var res = minimumLoss(prices.ToList());
            Assert.AreEqual(2, res);
        }

        public static int minimumLoss(List<long> price)
        {
            Dictionary<long, long> map = new Dictionary<long, long>();
            for (int i = 0; i < price.Count; i++)
            {
                map.Add(price[i], (long)i);
            }
            price.Sort();
            long minLoss = int.MaxValue;
            for (int i = 1; i < price.Count; i++)
            {
                if (price[i] > price[i - 1] && map[price[i]] < map[price[i - 1]])
                {
                    minLoss = Math.Min(minLoss, price[i] - price[i - 1]);
                }
            }

            return (int)minLoss;
        }
    }
}
