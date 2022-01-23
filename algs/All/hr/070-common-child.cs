using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoreLinq.Extensions;

namespace commonchild
{
    // https://www.hackerrank.com/challenges/common-child/problem?h_r=internal-search&isFullScreen=true
    // https://en.wikipedia.org/wiki/Longest_common_subsequence_problem#Example_in_C#_2

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var s1 = "HARRY";
            var s2 = "SALLY";

            var res = commonChild(s1, s2);

            Assert.AreEqual(2, res);
        }

        public static int commonChild(string s1, string s2)
        {
            int[][] dp = new int[s1.Length + 1][];
            for (int i = 0; i < s1.Length +1; i++)
            {
                dp[i] = new int[s2.Length + 1];
            }
            
            for (int i = 1; i <= s1.Length; i++)
            {
                for (int j = 1; j <= s2.Length; j++)
                {
                    if (s1[i - 1] == s2[j - 1])
                    {
                        dp[i][j] = dp[i - 1][j - 1] + 1;
                    }
                    else
                    {
                        dp[i][j] = Math.Max(dp[i - 1][j], dp[i][j - 1]);
                    }
                }
            }

            return dp[s1.Length][s2.Length];
        }

        static int LcsLength(string a, string b)
        {
            int m = a.Length;
            int n = b.Length;
            int[,] C = new int[m + 1, n + 1];
            for (int i = 0; i < m; i++)
                C[i, 0] = 0;
            for (int j = 0; j < n; j++)
                C[0, j] = 0;
            for (int i = 1; i <= m; i++)
            for (int j = 1; j <= n; j++)
            {
                if (a[i - 1] == b[j - 1])
                    C[i, j] = C[i - 1, j - 1] + 1;
                else
                    C[i, j] = Math.Max(C[i, j - 1], C[i - 1, j]);
            }
            return C[m, n];
        }

        string backtrack(int[,] C, char[] aStr, char[] bStr, int x, int y)
        {
            if (x == 0 | y == 0)
                return "";
            if (aStr[x - 1] == bStr[y - 1]) // x-1, y-1
                return backtrack(C, aStr, bStr, x - 1, y - 1) + aStr[x - 1]; // x-1
            if (C[x, y - 1] > C[x - 1, y])
                return backtrack(C, aStr, bStr, x, y - 1);
            return backtrack(C, aStr, bStr, x - 1, y);
        }


    }
}
