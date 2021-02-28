using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace All
{
    [TestClass]
    public class AhoCorasick
    {
        private static int ALPHABET_SIZE = 26;

        public static class Node
        {
            public static int parent;
            public static char charFromParent;
            public static int suffLink = 1;
            public static int[] children = new int [ALPHABET_SIZE];
            public static int[] transition = new int[ALPHABET_SIZE];
            public static bool leaf;

            static Node()
            {
                Array.Fill(children, -1);
                Array.Fill(transition, -1);
            }
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
