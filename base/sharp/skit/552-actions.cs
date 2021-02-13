using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace p552
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            string[] values = { "x", "y", "z" };
            var actions = new List<Action>();
            foreach (var v in values)
            {
                actions.Add(() => Console.WriteLine(v));
            }
            foreach (var a in actions)
            {
                a();
            }
        }
    }
}
