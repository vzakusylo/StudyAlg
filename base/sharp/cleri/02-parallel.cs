using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace p4_parralel1
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            Node root = new Node(3)
            {
                Left = new Node(4)
                {
                    Left = new Node(6),
                    Right = new Node(2)
                },
                Right = new Node(2)
                {
                    Left = new Node(3),
                    Right = new Node(1)
                }
            };

            ProcessTree(root);
        }

        void ProcessTree(Node root)
        {
            var task = Task.Factory.StartNew(() => Traverse(root),
                CancellationToken.None,
                TaskCreationOptions.AttachedToParent,
                TaskScheduler.Default);

            task.Wait();
        }

        void Traverse(Node node)
        {
            DoActionOnNode(node);
            if (node.Left != null)
            {
                Task.Factory.StartNew(
                    () => Traverse(node.Left),
                    CancellationToken.None,
                    TaskCreationOptions.AttachedToParent,
                    TaskScheduler.Default);
            }

            if (node.Right != null)
            {
                Task.Factory.StartNew(
                    () => Traverse(node.Right),
                    CancellationToken.None,
                    TaskCreationOptions.AttachedToParent,
                    TaskScheduler.Default
                );
            }
        }

        private void DoActionOnNode(Node node)
        {
            Console.WriteLine($"processing node {node.Data} on task id {Task.CurrentId}");
            Task.Delay(1000);
        }
    }

    public class Node
    {
        public Node(int d)
        {
            Data = d;
        }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public int Data { get; set; }
    }
}
