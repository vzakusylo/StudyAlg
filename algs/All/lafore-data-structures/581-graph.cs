using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace graph_581
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
            theGraph.AddEdge(1, 2);
            theGraph.AddEdge(0, 3);
            theGraph.AddEdge(3, 4);

            Console.WriteLine("Visits:");
            theGraph.Dfs();
           // theGraph.Bfs();
        }
    }

    // На графе контакты могут представляться вершинами, а дорожки — ребрами.
    class Graph
    {
        private int MAX_VERTS = 20;
        private Vertex [] vertexList;
        private int[,] adjMat;
        private int nVerts;
        private Stack<int> theStack = new Stack<int>();
        private Queue<int> theQueue = new Queue<int>();

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

        public int GetAdjUnvisitedVertex(int v)
        {
            for (int j = 0; j < nVerts; j++)
            {
                if (adjMat[v,j] == 1 && vertexList[j].WasVisited == false)
                {
                    return j; // Возвращает первую найденную вершину
                }               
            }
            return -1;  // таких вершин нет
        }

        public void Dfs()
        {
            vertexList[0].WasVisited = true;
            DisplayVertex(0);
            theStack.Push(0);
            
            while(theStack.Count() != 0)
            {
                int v = GetAdjUnvisitedVertex(theStack.Peek());
                if (v == -1)
                {
                    theStack.Pop();
                }
                else
                {
                    vertexList[v].WasVisited = true;
                    DisplayVertex(v);
                    theStack.Push(v);
                }
            }

            for (int i = 0; i < nVerts; i++)
            {
                vertexList[i].WasVisited = false;
            }
        }

        public void Bfs()
        {
            vertexList[0].WasVisited = true;
            DisplayVertex(0);
            theQueue.Enqueue(0);
            int v2;

            while (theQueue.Count != 0)
            {
                int v1 = theQueue.Dequeue();
                while ((v2=GetAdjUnvisitedVertex(v1)) != -1)
                {
                    vertexList[v2].WasVisited = true;
                    DisplayVertex(v2);
                    theQueue.Enqueue(v2);
                }
            }

            for (int i = 0; i < nVerts; i++)
            {
                vertexList[i].WasVisited = false;
            }
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

//При работе с графами обычно применяются две структуры: матрица смежности и список смежности.
//Обход в глубину реализуется на базе стека, а обход в ширину реализуется на базе очереди.