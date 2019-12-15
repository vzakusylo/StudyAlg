using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Template
{

    [MemoryDiagnoser]
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            BenchmarkRunner.Run<Program>();
        }
    }

    [TestClass]
    [MemoryDiagnoser]
    public class Program
    {
        //[TestMethod]
        //static void Main() => BenchmarkRunner.Run<Program>();

        private readonly Channel<int> s_channel = Channel.CreateUnbounded<int>();

        [Benchmark]
        public async Task WriteThenRead()
        {
            ChannelWriter<int> writer = s_channel.Writer;
            ChannelReader<int> reader = s_channel.Reader;
            for (int i = 0; i < 10_000_000; i++)
            {
                writer.TryWrite(i);
                await reader.ReadAsync();
            }
        }
    }
}
