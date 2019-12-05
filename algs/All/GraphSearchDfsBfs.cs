using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace GraphSearchDfsBfs
{
    //https://www.youtube.com/watch?v=zaBhtODEL0w
    [TestClass]
    public class Template
    {
        [TestMethod]
        public void Main()
        {
        }
    }

    public class Graph
    {
        private Dictionary<int, Node> nodeLookup = new Dictionary<int, Node>();

        public Node GetNode(int id) { return new Node(id); }

        public void AddEdge(int source, int destination)
        {
            Node s = GetNode(source);
            Node d = GetNode(destination);
            s.Adjacent.AddFirst(d);
        }

        public bool HasPathDfs(int source, int desctination)
        {
            Node s = GetNode(source);
            Node d = GetNode(desctination);
            HashSet<int> visited = new HashSet<int>();
            return HasPathDfs(s, d, visited);
        }

        public bool HasPathDfs(Node source, Node desctination, HashSet<int> visited)
        {
            if (visited.Contains(source.Id))
            {
                return false;
            }

            visited.Add(source.Id);
            if (source == desctination)
            {
                return true;
            }

            foreach (var child in source.Adjacent)
            {
                if (HasPathDfs(child, desctination, visited))
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasPathBfs(int source, int desctination)
        {
            return HasPathBfs(GetNode(source), GetNode(desctination));
        }

        private bool HasPathBfs(Node source, Node destination)
        {
            LinkedList<Node> nextToVisit = new LinkedList<Node>();
            HashSet<int> visited = new HashSet<int>();
            nextToVisit.AddFirst(source);
            while (nextToVisit.Any())
            {
                Node node = nextToVisit.First();
                nextToVisit.RemoveFirst();
                if (node == destination)
                {
                    return true;
                }

                if (visited.Contains(node.Id))
                {
                    continue;
                }

                visited.Add(node.Id);

                foreach (var node1 in node.Adjacent)
                {
                    nextToVisit.AddFirst(node1);
                }
            }
            return false;
        }
    }

    public class Node
    {

        public Node(int id)
        {
            Id = id;
        }
        public int Id { get; }
        public LinkedList<Node> Adjacent = new LinkedList<Node>();
    }
}
