using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace luckBalance
{
    // https://www.hackerrank.com/challenges/luck-balance

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            //int[][] a = new int[4][] {
            //    new int[]{33, 12},
            //    new int[]{12, 10},
            //    new int[]{13,5},
            //    new int[]{27, 19}
            //};

            var contests = new List<List<int>>
            { 
                new() { 5, 1}, 
                new() { 2, 1 },
                new() { 1, 1 },
                new() { 8, 1 },
                new() { 10, 0 },
                new() { 5, 0 },
            };

            var res = luckBalance(3, contests);
            Assert.AreEqual(29, res);

            contests = new List<List<int>>
            {
                
                new() { 13, 1},
                    new() { 10, 1},
                    new() { 9, 1},
                    new() { 8, 1},
                    new() { 13, 1},
                    new() { 12, 1 },
                    new() { 18, 1 },
                    new() { 13, 1 }
            };

            res = luckBalance(5, contests);
            Assert.AreEqual(42, res);
        }

        public static int luckBalance(int k, List<List<int>> contests)
        {
            int luckBalance = 0;
            //int[][] arr = new int [contests.Count][];
            //for (int i = 0; i < contests.Count; i++)
            //{
            //    arr[i] = new[] {contests[i][0], contests[i][1]};
            //}
            var arr = contests.Select(x => x.ToArray()).ToArray();

            JaggedComparer comp = new JaggedComparer();
            Array.Sort(arr, comp);

            for (int i = 0; i < arr.Length; i++)
            {
                int luck = arr[i][0];
                int importance = arr[i][1];

                if (importance == 1 && k > 0)
                {
                    k--;
                    luckBalance += luck;
                }
                else if(importance == 1 && k == 0)
                {
                    luckBalance -= luck;
                }

                if (importance == 0)
                {
                    luckBalance += luck;
                }
            }

            return luckBalance;
        }

        class JaggedComparer : IComparer
        {
            public int Compare(object x, object y)
            {
                // x and y are arrays of integers
                // sort on the 2nd item in each array
                int[] a1 = (int[])x;
                int[] a2 = (int[])y;
                return -1 * (a1[0].CompareTo(a2[0]));
            }
        }


        //int[] arr = new int[] { 1, 9, 6, 7, 5, 9 };

        //// Sort the arr from last to first
        //// Normal compare is m-n 
        //Array.Sort<int>(arr, delegate (int m, int n)
        //    { return n - m; });

        //// print all element of array
        //foreach (int value in arr)
        //{
        //    Console.Write(value + " ");
        //}

        //var arr = new int[contests.Count, 2];
        ////var arr1 = new int[,] { { 1, 2 }, { 3, 4 } };

        //for (int i = 0; i < contests.Count; i++)
        //{
        //    arr[i, 0] = contests[i][0];
        //    arr[i,1] = contests[i][1];
        //}



        //contests.OrderBy(x => x[0]); 
        //int[,] sortedBySecondElement = contests.OrderBy(x => x[0]); 
        //int[,] sortedByThirdElement = contests.OrderBy(x => x[1]);

        ////var l = new Comparison<List<List<int>>() 
        //var l1 = new Func<int, int, int>((a,b) => a + b);
        //var l2 = new Func<List<List<int>>, List<List<int>>>((a, b) => );

        //Array.Sort(contests, new Comparison<List<List<int>>() = (a,b) => {});


        //Array.Sort(contests, (p,q) => p.CmpareTo(q));
    }
}
