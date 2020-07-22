using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Threading.Channels;
using System.Threading.Tasks;
using System.IO.Pipelines;
using System.IO;
using System.Buffers;

namespace Pipeline
{

   
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public async Task Main()
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(Guid.NewGuid());
            writer.Flush();
            stream.Position = 0;
            var reader = PipeReader.Create(stream);
            ReadResult result = await reader.ReadAsync();
            ReadOnlySequence<byte> seq;
        }
    }

   
    
}
