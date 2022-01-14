using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ctcicomparatorsorting
{
    //https://www.hackerrank.com/challenges/ctci-comparator-sorting

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var list = new List<Player>()
            {
                new("davis", 15),
                new("davis", 20),
                new("davis", 10),
                new("edgehill", 15)
            };

            var arr = new Player[]
            {
                new("davis", 15),
                new("davis", 20),
                new("davis", 10),
                new("edgehill", 15)
            };

            Array.Sort(arr, new PlayerComparer());

            Assert.IsTrue(arr[0].score == 20 && arr[0].name == "davis");
            Assert.IsTrue(arr[1].score == 15 && arr[1].name == "davis");
            Assert.IsTrue(arr[2].score == 15 && arr[2].name == "edgehill");
            Assert.IsTrue(arr[3].score == 10 && arr[3].name == "davis");
        }
    }

    class PlayerComparer : IComparer<Player>
    {
        public int Compare(Player a, Player b)
        {
            if (a.score == b.score)
            {
                return a.name.CompareTo(b.name);
            }
            return b.score - a.score;
        }
    }


    class Player
    {
        public String name;
        public int score;

        public Player(String name, int score)
        {
            this.name = name;
            this.score = score;
        }
    }
}
