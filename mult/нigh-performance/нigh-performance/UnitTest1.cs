using System;

using Xunit;

namespace нigh_performance
{
    using System.IO.Pipelines;
    using System.Threading.Tasks;

    //https://habr.com/en/post/464921/
    //https://www.nuget.org/packages/System.IO.Pipelines/
    public class Article01
    {

        [Fact]
        public async Task SimpleInit()
        {
            var pipe = new Pipe();
            PipeWriter pipeWriter = pipe.Writer;
            await pipeWriter.WriteAsync(new ReadOnlyMemory<byte>(new byte[0]));
            var mem = pipeWriter.GetMemory();
            //Span<byte> span = pipeWriter.GetSpan();
            pipeWriter.Advance(0);
            await pipeWriter.FlushAsync();
            //pipeWriter.Complete(new ArgumentException());
            pipeWriter.Complete();
            pipeWriter.CancelPendingFlush();

            PipeReader pipeReader = pipe.Reader;
            pipeReader.AdvanceTo(new SequencePosition());
            await pipeReader.ReadAsync();
            pipeReader.TryRead(out ReadResult result);
            pipeReader.Complete();
            pipeReader.CancelPendingRead();
            
        }
    }
}
