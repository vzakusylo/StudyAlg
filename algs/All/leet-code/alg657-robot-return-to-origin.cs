using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using shared;


namespace robotreturntoorigin
{
    // https://leetcode.com
    [TestClass]
    public class Solution
    {
       
        [TestMethod]
        public void Main()
        {
            var moves = "UD";
            var res = JudgeCircle(moves);
            Assert.IsTrue(res);
        }

        public bool JudgeCircle(string moves)
        {
            int x = 0;
            int y = 0;

            foreach (var move in moves){
                if (move == 'U')
                {
                    y++;
                }
                if (move == 'D')
                {
                    y--;
                }
                if (move == 'L')
                {
                    x--;
                }
                if (move == 'R')
                {
                    x++;
                }
            }

            return x == 0 && y == 0;
        }

    }
}
