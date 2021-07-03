using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AhoCorasick
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            AhoCorasick ahoCorasick = new AhoCorasick(1000);
            ahoCorasick.addString("bc");
            ahoCorasick.addString("abc");

            string s = "tabcdc";
            int node = 0;
            List<int> positions = new List<int>();
            for (int i = 0; i < s.Length; i++)
            {
                node = ahoCorasick.transition(node, s[i]);
                if (ahoCorasick.nodes[node].leaf)
                {
                    positions.Add(i);
                }
            }
            Console.WriteLine(positions);
        }
    }


    public class AhoCorasick
    {
        private static int ALHAPHBET_SIZE = 26;
        public Node[] nodes;
        private int nodeCount;

        public class Node
        {
            public int parent;
            public char charFromParent;
            public int suffLink = -1;
            public int[] children = new int[ALHAPHBET_SIZE];
            public int[] transitions = new int[ALHAPHBET_SIZE];
            public bool leaf;

            public Node()
            {
                Array.Fill(children, -1);
                Array.Fill(transitions, -1);
            }
        }

        public AhoCorasick(int maxNodes)
        {
            nodes = new Node[maxNodes];

            nodes[0] = new Node();
            nodes[0].suffLink = 0;
            nodes[0].parent = -1;
            nodeCount = 1;
        }

        public void addString(string s)
        {
            int cur = 0;
            foreach (var ch in s.ToCharArray())
            {
                int c = ch - 'a';
                if (nodes[cur].children[c] == -1)
                {
                    nodes[nodeCount] = new Node();
                    nodes[nodeCount].parent = cur;
                    nodes[nodeCount].charFromParent = ch;
                    nodes[cur].children[c] = nodeCount++;
                }

                cur = nodes[cur].children[c];
            }
            nodes[cur].leaf = true;
        }

        public int suffLink(int nodeIndex)
        {
            var node = nodes[nodeIndex];
            if (node.suffLink == -1)
            {
                node.suffLink = node.parent == 0 ? 0 : transition(suffLink(node.parent), node.charFromParent);
            }
            return node.suffLink;
        }

        public int transition(int nodeIndex, char ch)
        {
            int c = ch - 'a';
            Node node = nodes[nodeIndex];
            if (node.transitions[c] == -1)
            {
                node.transitions[c] = node.children[c] != -1
                    ? node.children[c]
                    : (nodeIndex == 0 ? 0 : transition(suffLink(nodeIndex), ch));
            }

            return node.transitions[c];
        }
    }
}
