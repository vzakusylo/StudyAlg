using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace sock_merchant
{
    //https://www.hackerrank.com/challenges/sock-merchant/problem?utm_campaign=challenge-recommendation&utm_medium=email&utm_source=24-hour-campaign

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            int result = sockMerchant(9, new[] { 10, 20, 20, 10, 10, 30, 50, 10, 20 });
            Assert.AreEqual(3, result);
        }

        static int sockMerchant(int n, int[] ar)
        {
            var pairs = new Dictionary<int, int>();
            foreach (var item in ar)
            {
                if (pairs.ContainsKey(item))
                {
                    var count = pairs[item];
                    pairs[item] = ++count;
                }
                else
                {
                    pairs.Add(item, 1);
                }
            }
            var result = 0;
            foreach (var pair in pairs)
            {   
               result += pair.Value / 2;
            }
            return result;
        }
    }
}
