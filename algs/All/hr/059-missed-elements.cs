using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace missingnumbers
{
    // https://www.hackerrank.com/challenges/missing-numbers

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var arr = new List<int>() {203, 204,      205, 206, 207,      208, 203, 204, 205, 206};
            var brr = new List<int>() {203, 204, 204, 205, 206, 207, 205, 208, 203, 206, 205, 206, 204};

            var res = missingNumbers(arr, brr);
            Assert.AreEqual(new List<int>{ 204, 205, 206 }, res);
        }

        public static List<int> missingNumbers(List<int> arr, List<int> brr)
        {
            Dictionary<int, int> aMap = new Dictionary<int, int>();
            Dictionary<int, int> bMap = new Dictionary<int, int>();

            for (int i = 0; i < arr.Count; i++)
            {
                if (aMap.ContainsKey(arr[i]))
                {
                    aMap[arr[i]] += 1;
                }
                else
                {
                    aMap[arr[i]] = 1;
                }
            }

            for (int i = 0; i < brr.Count; i++)
            {
                if (bMap.ContainsKey(brr[i]))
                {
                    bMap[brr[i]] += 1;
                }
                else
                {
                    bMap[brr[i]] = 1;
                }
            }

            List<int> list = new List<int>();
            foreach (var element in bMap.Keys)
            {
                if (aMap.ContainsKey(element))
                {
                    if (bMap[element] > aMap[element])
                    {
                        list.Add(element);
                    }
                }
                else
                {
                    list.Add(element);
                }
            }

            list.Sort();
            return list;

        }

    }
}
