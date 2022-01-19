using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace connectedcellinagrid
{
    // https://www.hackerrank.com/challenges/connected-cell-in-a-grid

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var matrix = new List<List<int>>
            {
                new() {1, 1, 0, 0, 0},
                new() {0, 1, 1, 0, 0},
                new() {0, 0, 1, 0, 1},
                new() {1, 0, 0, 0, 1},
                new() {0, 1, 0, 1, 1}
            };

            var res  = connectedCell(matrix);

            Assert.AreEqual(5, res);


            

            matrix = new List<List<int>>
            {
                new() {0, 1, 0, 0, 0, 0, 1, 1, 0},
                new() {1, 1, 0, 0, 1, 0, 0, 0, 1},
                new() {0, 0, 0, 0, 1, 0, 1, 0, 0},
                new() {0, 1, 1, 1, 0, 1, 0, 1, 1},
                new() {0, 1, 1, 1, 0, 0, 1, 1, 0},
                new() {0, 1, 0, 1, 1, 0, 1, 1, 0},
                new() {0, 1, 0, 0, 1, 1, 0, 1, 1},
                new() {1, 0, 1, 1, 1, 1, 0, 0, 0},

            };

            res = connectedCell(matrix);

            Assert.AreEqual(29, res);


        }

        public static int connectedCell(List<List<int>> matrix)
        {
            // matrix dfs problem        
            int maxConnectedCells = 0;
            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix[i].Count; j++)
                {
                    if (matrix[i][j] == 1)
                    {
                        dfs(matrix, i, j, 1, ref maxConnectedCells);
                    }
                }
            }

            return maxConnectedCells;
        }

        private static void dfs(List<List<int>> matrix, int i, int j, int currConnCells, ref int maxConnectedCells)
        {
            if (i <= 0 || i >= matrix.Count || j <= 0 || j >= matrix[i].Count ||
                matrix[i][j] == 0)
            {
                return;
            }

            matrix[i][j] = 0;
            currConnCells++;
            maxConnectedCells = Math.Max(maxConnectedCells, currConnCells);
            dfs(matrix, i, j - 1, currConnCells, ref maxConnectedCells);
            dfs(matrix, i, j + 1, currConnCells, ref maxConnectedCells);
            dfs(matrix, i - 1, j, currConnCells, ref maxConnectedCells);
            dfs(matrix, i + 1, j, currConnCells, ref maxConnectedCells);
            dfs(matrix, i - 1, j - 1, currConnCells, ref maxConnectedCells);
            dfs(matrix, i - 1, j + 1, currConnCells, ref maxConnectedCells);
            dfs(matrix, i + 1, j - 1, currConnCells, ref maxConnectedCells);
            dfs(matrix, i + 1, j + 1, currConnCells, ref maxConnectedCells);

        }


    }
}
