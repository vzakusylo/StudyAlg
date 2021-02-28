using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mst
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            Graph theGraph = new Graph();
            theGraph.AddVertex('A');
            theGraph.AddVertex('B');
            theGraph.AddVertex('C');
            theGraph.AddVertex('D');
            theGraph.AddVertex('E');
            theGraph.AddEdge(0, 1);
            theGraph.AddEdge(0, 2);
            theGraph.AddEdge(0, 3);
            theGraph.AddEdge(0, 4);
            theGraph.AddEdge(1, 2);
            theGraph.AddEdge(1, 3);
            theGraph.AddEdge(1, 4);
            theGraph.AddEdge(2, 3);
            theGraph.AddEdge(2, 4);
            theGraph.AddEdge(3, 4);

            Console.WriteLine("Minimum spanning tree");
            theGraph.Mst();
        }
    }

    class Graph
    {
        private const int MAX_VERTS = 20;
        private Vertex [] vertexList;
        private int[,] adjMat;
        private int nVerts;
        private StackX theStack;
        
        public Graph()
        {
            vertexList = new Vertex[MAX_VERTS];
            adjMat = new int[MAX_VERTS, MAX_VERTS];
            nVerts = 0;
            for (int j = 0; j < MAX_VERTS; j++)
            {
                for (int k = 0; k < MAX_VERTS; k++)
                {
                    adjMat[j, k] = 0;
                }
            }
            theStack = new StackX();
        }

        public void AddVertex(char label)
        {
            vertexList[nVerts++] = new Vertex(label);
        }

        public void AddEdge(int start, int end)
        {
            adjMat[start, end] = 1;
            adjMat[end, start] = 1;
        }

        public void Mst()
        {
            vertexList[0].wasVisited = true;
            theStack.Push(0);
            while (!theStack.IsEmpty)
            {
                int currentVertex = theStack.Peek();
                int v = GetAdjUnvisitedVertex(currentVertex);
                if (v == -1)                
                    theStack.Pop();
                else
                {
                    vertexList[v].wasVisited = true;
                    theStack.Push(v);

                    DisplayVertex(currentVertex,v);
                }
            }

            for (int i = 0; i < nVerts; i++)
            {
                vertexList[i].wasVisited = false;
            }
        }

        private void DisplayVertex(int current, int v)
        {
            Console.Write($"{vertexList[current].Label}{vertexList[v].Label} ");
        }

        private int GetAdjUnvisitedVertex(int v)
        {
            for (int i = 0; i < nVerts; i++)            
                if (adjMat[v,i] == 1 && vertexList[i].wasVisited == false)                
                    return i;
            return -1;
        }
    }

    class Vertex
    {
        public bool wasVisited { get; set; }
        public char Label { get; }

        public Vertex(char label)
        {
            this.Label = label;
        }
    }

    class StackX
    {
        private const int SIZE = 20;
        private int[] st;
        private int top;

        public StackX()
        {
            st = new int [SIZE];
            top = -1;
        }

        public void Push(int j) =>        
            st[++top] = j;
        
        public int Pop() =>        
             st[top--];

        public int Peek() => st[top];

        public bool IsEmpty => top == -1;
    }
}
