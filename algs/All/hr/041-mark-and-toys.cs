using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace markandtoys
{
    // https://www.hackerrank.com/challenges/mark-and-toys

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
           
        }

        public static int maximumToys(List<int> prices, int k)
        {
            int maxToys = 0;
            if (prices.Count == 0 || k == 0)
                return maxToys;

            prices.Sort();

            for (int i = 0; i < prices.Count; i++)
            {
                k -= prices[i];
                if (k < 0)
                {
                    return maxToys;
                }

                maxToys++;
            }

            return maxToys;
        }


    }
}
