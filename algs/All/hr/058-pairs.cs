using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace pairs
{
    // https://www.hackerrank.com/challenges/pairs/problem?h_r=internal-search&isFullScreen=true

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var arr = new[] {1, 5, 3, 4, 2};
            var res = pairs(2, arr.ToList());
            Assert.AreEqual(3, res);
        }

        public static int pairs(int k, List<int> arr)
        {
            // time complexity o(n)

            //  1 5 3 4 2 => hashmap
            //  1 => 1 + k = 3
            //  1 - k = -1

            Dictionary<int, int> map = new Dictionary<int, int>();
            for (int i = 0; i < arr.Count; i++)
            {
                var item = arr[i];
                if (map.ContainsKey(item))
                {
                    map.Add(item, map[item] + 1);
                }
                else
                {
                    map.Add(item, 1);
                }
            }

            int pairsCount = 0;
            for (int i = 0; i < arr.Count; i++)
            {
                var item = arr[i];
                int target = item + k; // arr[i] + k - arr[i] = k
                if (map.ContainsKey(target))
                {
                    pairsCount += map[item] * map[target];
                }

                target = item - k; // arr[i] - (arr[i] - k) = k
                if (map.ContainsKey(target))
                {
                    pairsCount += map[item] * map[target];
                }

                map.Remove(item);
            }

            return pairsCount;
        }


    }
}
