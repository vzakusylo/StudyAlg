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
            //  0 0 0 0 0
            //0 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15
            //-----''----'
            var res = hackerlandRadioTransmitters(new[] { 1, 2, 3, 4, 5 }, 1);
            Assert.AreEqual(2, res);

            res = hackerlandRadioTransmitters(new[] { 7, 2, 4, 6, 5, 9, 12, 11 }, 2);
            Assert.AreEqual(3, res);

            res = hackerlandRadioTransmitters(new[] { 9, 5, 4, 2, 6, 15, 12 }, 2);
            Assert.AreEqual(4, res);

            res = hackerlandRadioTransmitters(new[] { 2, 2, 2, 2, 1, 1, 1, 1 }, 2);
            Assert.AreEqual(1, res);

            res = hackerlandRadioTransmitters(new[] { 10,10,10 }, 3);
            Assert.AreEqual(1, res);

            //            8 2
            //2 2 2 2 1 1 1 1

            //   0   0 0 0     0       0        0
            // 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15
        }




        static int hackerlandRadioTransmitters1(int[] x, int k)
        {
            int numOfTransmitters = 0;
            int i = 0;
            int n = x.Length;
            while (i < n)
            {
                numOfTransmitters++;
                int loc = x[i] + k;
                while (i < n && x[i] <= loc) i++;
                loc = x[--i] + k;
                while (i < n && x[i] <= loc) i++;
            }
            return numOfTransmitters;
        }


            static int hackerlandRadioTransmitters(int[] x, int k)
        {
            x = x.Distinct().ToArray();
            Array.Sort(x);
            var maxCoverage = k * 2 + 1; // 5
            var minElem = x.Min(); // 2
            var maxElem = x.Max(); // 12

            if (x.Length == 1)
            {
                return 1;
            }

           var dict = Enumerable.Range(x.Min(), x.Max()).ToDictionary(d => d, d=> new {has = false, addr = d, from = 0,  to = 0 });
            x.Select(a => dict[a] = new { has = true, addr = a, from = a-k < 0 ? minElem : a-k, to  = a + k }).ToArray();
                       
            var fillGroups = new List<((int from, int to), int)>();
            var first = dict.Where(d => d.Value.has && d.Value.from == dict.Values.Where(v => v.has).Min(v => v.addr));
            var firstItem = (first.First().Value.from, first.First().Value.to);
            fillGroups.Add((firstItem, 1));

            foreach (var g in dict)
            {
                var ranges = dict.Values.Where(a => g.Value.to >= a.addr && g.Value.from <= a.addr);
                var item = (g.Value.from, g.Value.to);
                
                if (!fillGroups.Any(fg=> fg.Item1.to >= g.Value.from && fg.Item1.from <= g.Value.to) && g.Value.has) 
                {
                    fillGroups.Add((item, ranges.Count()));
                }
                
                else if (g.Value.has && g.Value.addr > fillGroups.Select(fg => fg.Item1.to).Max())
                {
                    fillGroups.Add((item, ranges.Count()));
                }
               
            }

            return fillGroups.Count;
        }
    }
}
