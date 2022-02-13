using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

//https://drive.google.com/file/d/0B7EfQdvL5qf_Q2JCWmZJRHQwdFU/view

namespace pascals_triangle
{
    // https://leetcode.com/problems/pascals-triangle/
    [TestClass]
    public class LongestPalindromicSubstringNativApproach
    {
        [TestMethod]
        public void Main()
        {

            var res = Generate(2);
            Assert.AreEqual(2, res.Count);
        }

        public IList<IList<int>> Generate(int numRows)
        {
            IList<IList<int>> triangle = new List<IList<int>>();
            if (numRows == 0) return triangle;

            List<int> firstRow = new List<int>();
            firstRow.Add(1);
            triangle.Add(firstRow);

            for (int i = 1; i < numRows; i++)
            {
                IList<int> prevRow = triangle[i - 1];
                List<int> row = new List<int>();

                row.Add(1);

                for (int j = 1; j < i; j++)
                {
                    row.Add(prevRow[j - 1] + prevRow[j]);
                }

                row.Add(1);
                triangle.Add(row);
            }

            return triangle;
        }
    }
}
