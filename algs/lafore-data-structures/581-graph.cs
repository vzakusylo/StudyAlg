using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace graph_581
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {          
            
        }
    }

    // На графе контакты могут представляться вершинами, а дорожки — ребрами.
    class Graph
    {
        private int MAX_VERTS = 20;
        private Vertex [] vertexList;
        private int[,] adjMat;
        private int nVerts;

        public Graph()
        {
            vertexList = new Vertex[MAX_VERTS];
            adjMat = new int[MAX_VERTS, MAX_VERTS];
            nVerts = 0;
            for (int j = 0; j < MAX_VERTS; j++)
                for (int k = 0; k < MAX_VERTS; k++)
                    adjMat[k, j] = 0;
        }

        public void AddVertex(char lab)
        {
            vertexList[nVerts++] = new Vertex(lab);
        }

        public void AddEdge(int start, int end)
        {
            adjMat[start, end] = 1;
            adjMat[end, start] = 1;
        }

        public void DisplayVertex(int v)
        {
            Console.WriteLine(vertexList[v].Label);
        }

    }
    // Вершины графа моделировали участки земли, а ребра — мосты

    class Vertex //вершина
    {        
        public bool WasVisited { get; set; }
        public char Label { get; private set; }

        public Vertex(char lab)
        {
            Label = lab;
        }
    }
}
