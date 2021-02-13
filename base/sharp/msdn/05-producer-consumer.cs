using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace producer_concumer_05
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            ProcessFile("", "");
        }

        static void ProcessFile(string input, string output)
        {
            var inputsLine = new System.Collections.Concurrent.BlockingCollection< string>();
            var processedLines = new System.Collections.Concurrent.BlockingCollection<string>();

            // stage #1
            var readLines = Task.Factory.StartNew(() =>
            {
                try
                {
                    foreach (var line in File.ReadLines(input)) inputsLine.Add(line);
                }
                finally { inputsLine.CompleteAdding(); }
            });

            // stage #2
            var processLines = Task.Factory.StartNew(() =>
            {
                try
                {
                    foreach (var line in inputsLine.GetConsumingEnumerable()
                    .Select(line=> Regex.Replace(line, @"\s+", ", ")))
                    {
                        processedLines.Add(line);
                    }
                }
                finally { processedLines.CompleteAdding(); }
            });

            // stage #3
            var writeLines = Task.Factory.StartNew(() =>
            {
                File.WriteAllLines(output, processedLines.GetConsumingEnumerable());
            });

            Task.WaitAll(readLines, processLines, writeLines);
        }

        class BlockingQueue<T>
        {
            private Queue<T> _queue = new Queue<T>();
            private SemaphoreSlim _semaphore = new SemaphoreSlim(0, int.MaxValue);

            public void Enqueue(T data)
            {
                if (data == null) throw new ArgumentNullException("data");
                lock (_queue) _queue.Enqueue(data);
                _semaphore.Release();
            }

            public T Dequeue()
            {
                _semaphore.Wait();
                lock (_queue) return _queue.Dequeue();
            }
        }
    }
}
