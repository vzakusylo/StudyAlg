using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

//https://drive.google.com/file/d/0B7EfQdvL5qf_Q2JCWmZJRHQwdFU/view

namespace numberofislands
{
    // https://leetcode.com/problems/number-of-islands/
    [TestClass]
    public class LongestPalindromicSubstringNativApproach
    {
        [TestMethod]
        public void Main()
        {
            char[][] grid = {
                new [] { '1', '1', '1', '1', '0' }, 
                new [] { '1', '1', '0', '1', '0' },
                new [] { '1', '1', '0', '0', '0' },
                new [] { '0', '0', '0', '0', '0' }
            };

            var res = NumIslands(grid);
            Assert.AreEqual(1, res);
        }

        public int NumIslands(char[][] grid)
        {
            int count = 0;

            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (grid[i][j] == '1')
                    {
                        count++;
                        callBFS(grid, i, j);
                    }
                }
            }

            return count;

        }

        public void callBFS(char[][] grid, int i, int j)
        {

            if (i < 0 || i >= grid.Length || j < 0 || j > grid[i].Length || grid[i][j] == '0')
            {
                return;
            }

            grid[i][j] = '0';
            callBFS(grid, i + 1, j); // up
            callBFS(grid, i - 1, j); // down
            callBFS(grid, i, j - 1); // left
            callBFS(grid, i, j + 1); // right
        }
    }
}
