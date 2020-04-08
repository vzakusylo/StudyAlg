using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace dataflowblock03
{
    // https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/how-to-write-messages-to-and-read-messages-from-a-dataflow-block

    [TestClass]
    public class Solution
    {

        [TestMethod]
        public async Task WritingReadingFromDataflowBlockAsynchronously()
        {
            var bufferBlock = new BufferBlock<int>();
            for (int i = 0; i < 3; i++)
            {
                await bufferBlock.SendAsync(i);
            }
            for (int i = 0; i < 3; i++)
            {
               Console.WriteLine(await bufferBlock.ReceiveAsync());
            }
        }

        [TestMethod]
        public void WriteToTheMessageBlockConcurrently()
        {
            var bufferBlock = new BufferBlock<int>();

            var post1 = Task.Run(() => {
                bufferBlock.Post(1);
                bufferBlock.Post(2);
            });
            var receive = Task.Run(() => {
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine(bufferBlock.Receive());
                }
            });
            var post2 = Task.Run(() => {
                bufferBlock.Post(3);
            });
            Task.WaitAll(post1, post2, receive);
        }

        [TestMethod]
        public void OccasionallyPollForData()
        {
            var bufferBlock = new BufferBlock<int>();
            for (int i = 0; i < 3; i++)
            {
                bufferBlock.Post(i);
            }

            int value;
            while (bufferBlock.TryReceive(out value))
            {
                Console.WriteLine(value);
            }
        }

        [TestMethod]
        public void SameObject()
        {
            var bufferBlock = new BufferBlock<int>();
            for (int i = 0; i < 3; i++)
            {
                bufferBlock.Post(1);
            }
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(bufferBlock.Receive());
            }
        }
    }
}