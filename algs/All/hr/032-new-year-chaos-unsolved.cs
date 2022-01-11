using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace newYearChaos
{
    // https://www.hackerrank.com/challenges/new-year-chaos

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var q = new[] { 1, 2, 5, 3, 4, 7, 8, 6 };
            var res = MinimumBribes(q.ToList());
            Assert.AreEqual("4", res);

            q = new[] { 5,1,2,3,7,8,6,4 };
            res = MinimumBribes(q.ToList());
            Assert.AreEqual("Too chaotic", res);

            q = new[] {1, 2, 5, 3, 7, 8, 6, 4};
            res = MinimumBribes(q.ToList());
            Assert.AreEqual("7", res);

            q = new[] { 2, 1, 5, 3, 4};
            res = MinimumBribes(q.ToList());
            Assert.AreEqual("3", res);

            q = new[] { 2,5,1,3,4 };
            res = MinimumBribes(q.ToList());
            Assert.AreEqual("Too chaotic", res);

        }

        public static string MinimumBribes(List<int> q)
        {
            int count = 0;
            var missedNumbers = new List<int>();
            for (int i = 0; i < q.Count; i++)
            {
                var current = q[i];
                var index = i + 1;

                var diff = current - index;
                if (diff > 2)
                {
                    return "Too chaotic";
                }

                int? removeIndex = null;
                for (int j = 0; j < missedNumbers.Count; j++)
                {
                    var missedIndex = j;
                    var num = missedNumbers[j];
                    if (current == num)
                    {
                        removeIndex = missedIndex;
                    }
                }

                if (removeIndex.HasValue)
                {
                    missedNumbers.Remove(missedNumbers[removeIndex.Value]);
                }

                for (int k = 0; k < diff; k++)
                {
                    if (missedNumbers.Count < 2)
                    {
                        var missedNumber = index + k;
                        var isFoundBehind = false;

                        for (int b = i - 1; b > i -3; b--)
                        {
                            if (q[b] == missedNumber)
                            {
                                isFoundBehind = true;
                                break;
                            }
                        }

                        if (! isFoundBehind)
                        {
                            missedNumbers.Add(missedNumber);
                        }
                    }
                }

                for (int j = 0; j < missedNumbers.Count; j++)
                {
                    if (current > missedNumbers[j])
                    {
                        count++;
                    }
                }
            }

            return count.ToString();

        }

        // Space complexity = O(1)
            // Time complexity = O (n ^ 2 ) => O(n log n) || O(n)
            public static void MinimumBribes1(List<int> q)
        {
            var count = 0;
            for (int i = 0; i < q.Count; i++)
            {   
                var number = q[i];

                var diff = 0;
                for (int k = i+1; k < q.Count; k++)
                {
                    if (number > q[k])
                    {
                        diff += 1;
                    }
                }

                if (diff > 2)
                {
                    Console.WriteLine("Too chaotic");
                    return;
                }
                else
                {
                    count += diff;
                }
            }
            Console.WriteLine(count.ToString());
        }
    }
}
