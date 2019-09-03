using System;
using System.Buffers;
using System.IO;
using System.IO.Pipelines;
using System.IO.Pipes;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace example
{
    using System.Threading.Tasks;

    [TestClass]
    public class SystemIoPipelines
    {
        [TestMethod]
        public async Task Main()
        {
            var pipe = new Pipe();
            var dataWriter = new PipeDataWriter(pipe.Writer, "testpipe");
            var dataProcessor = new DataProcessor(new ConsoleBytesProcessor(), pipe.Reader);
            var cts = new CancellationTokenSource();
            await Task.WhenAll(dataWriter.ReadFromPipeAsync(cts.Token),
                dataProcessor.StartProcessingDataAsync(cts.Token));
        }
    }

    public class ConsoleBytesProcessor : IBytesProcessor
    {
        readonly FileStream _fileStream = new FileStream("buffer", FileMode.Create);
        public Task ProcessBytesAsync(ReadOnlySequence<byte> bytesSequence, CancellationToken token)
        {
            if (bytesSequence.IsSingleSegment)
            {
                ProcessSingle(bytesSequence.First.Span);
            }
            else
            {
                foreach (ReadOnlyMemory<byte> segment in bytesSequence)
                {
                    ProcessSingle(segment.Span);
                }
            }

            return Task.CompletedTask;
        }

        private void ProcessSingle(in ReadOnlySpan<byte> firstSpan)
        {
            _fileStream.Write(firstSpan);
        }
    }

    public class DataProcessor
    {
        private readonly IBytesProcessor _bytesProcessor;
        private readonly PipeReader _pipeReader;

        public DataProcessor(IBytesProcessor bytesProcessor, PipeReader pipeReader)
        {
            _bytesProcessor = bytesProcessor ?? throw new ArgumentNullException(nameof(bytesProcessor));
            _pipeReader = pipeReader ?? throw new ArgumentNullException(nameof(pipeReader));
        }

        public async Task StartProcessingDataAsync(CancellationToken token)
        {
            while (true)
            {
                token.ThrowIfCancellationRequested();
                ReadResult result = await _pipeReader.ReadAsync(token);
                ReadOnlySequence<byte> buffer = result.Buffer;
                await _bytesProcessor.ProcessBytesAsync(buffer, token);
                _pipeReader.AdvanceTo(buffer.End);
                if (result.IsCompleted)
                {
                    break;
                }
            }
            _pipeReader.Complete();
        }
    }

    public interface IBytesProcessor
    {
        Task ProcessBytesAsync(ReadOnlySequence<byte> bytesSequence, CancellationToken token);
    }

    public class PipeDataWriter
    {
        private readonly NamedPipeClientStream _namedPipe;
        private readonly PipeWriter _pipeWriter;
        private const string Servername = ".";

        public PipeDataWriter(PipeWriter pipeWriter, string pipeName)
        {
            _pipeWriter = pipeWriter ?? throw new ArgumentNullException(nameof(pipeWriter));
            _namedPipe = new NamedPipeClientStream(Servername, pipeName, PipeDirection.In);
        }

        public async Task ReadFromPipeAsync(CancellationToken token)
        {
            await _namedPipe.ConnectAsync(token);
            while (true)
            {
                token.ThrowIfCancellationRequested();
                int readBytes = _namedPipe.Read(_pipeWriter.GetSpan());
                if (readBytes == 0)
                {
                    await Task.Delay(500, token);
                    continue;
                }

                _pipeWriter.Advance(readBytes);

                FlushResult result = await _pipeWriter.FlushAsync(token);
                if (result.IsCanceled)
                {
                    break;
                }
            }
            _pipeWriter.Complete();
        }
    }
}

namespace нigh_performance
{
    using System.IO.Pipelines;
    using System.Threading.Tasks;

    //https://habr.com/en/post/464921/
    //https://www.nuget.org/packages/System.IO.Pipelines/
    [TestClass]
    public class SystemIOPipelines
    {
        [TestMethod]
        public async Task SimpleInit()
        {
            var pipe = new Pipe();
            PipeWriter pipeWriter = pipe.Writer;
            ValueTask<FlushResult> res =  pipeWriter.WriteAsync(new ReadOnlyMemory<byte>(new byte[0]));
            await res;
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

        [TestMethod]
        public void GetMemory() // #2 Memory<byte> GetMemory(int sizeHint)
        {
            var pipeNoOptions = new Pipe();

            Memory<byte> memoryOne = pipeNoOptions.Writer.GetMemory(2);
            Console.WriteLine(memoryOne.Length);

            var pipeWithOptions = new Pipe(new PipeOptions(minimumSegmentSize:5));

            Memory<byte> memoryTwo = pipeWithOptions.Writer.GetMemory(2);
            Console.WriteLine(memoryTwo.Length);

            #region Info

            //Но все в этом примере объяснимо и просто. 
            //При создании экземпляра Pipe мы можем передать ему в конструктор объект PipeOptions с опциями для создания.
            //В PipeOptions есть поле минимального размера сегмента по умолчанию.Еще не так давно оно было 2048, но данный коммит все изменил,
            // теперь 4096.На момент написания статьи версия с 4096 была пререлизным пакетом, в последней релизной версии было значение 2048.
            // Это объясняет поведение первого примера. Если вам критично использование меньшего размера для стандартного буфера, его можно указать в экземпляре типа PipeOptions.
            // Но во втором примере, где указан минимальный размер, длина все равно ему не соответствует. А это уже происходит потому,
            // что создание нового BufferSegment происходит с использованием пулов. Одной из опций в PipeOptions является пул памяти.
            // После этого для создания нового сегмента будет использоваться именно указанный пул.
            // Если вы не указали свой пул памяти, будет использоваться стандартный ArrayPool, который, как известно, имеет несколько бакетов под разные размеры массивов(каждый следующий в 2 раза больше предыдущего)
            // и при запросе на определенный размер, он ищет бакет с массивами подходящего размера(то есть ближайшего большего или равного). Соответственно, новый буфер почти наверно будет большего размера,
            // чем вы запросили.Минимальный размер массива в стандартном ArrayPool(System.Buffers.TlsOverPerCoreLockedStacksArrayPool) — 16.Но не стоит переживать, ведь это пул массивов.
            // Соответственно, в подавляющем большинстве случаев массив не оказывает нагрузки на сборщик мусора и будет использоваться повторно.

            #endregion
        }

        [TestMethod]
        public void GetSpan()
        {
            var pipe = new Pipe();
            var pipeWriter = pipe.Writer;

            // Таким образом GetMemory() или GetSpan() — главные методы для записи. 
            Span<byte> res = pipeWriter.GetSpan();
        }

        [TestMethod]
        public void GetAdvance()
        {
            var pipe = new Pipe();
            var pipeWriter = pipe.Writer;

            // Таким образом GetMemory() или GetSpan() — главные методы для записи. 
            Span<byte> res = pipeWriter.GetSpan();

            // принимает количество записанных байт. 
            pipeWriter.Advance(5);
            // И все тело метода является критической секцией и выполняется под локом.
        }


        [TestMethod]
        public void FlushAsync()
        {
            var pipe = new Pipe();
            var pipeWriter = pipe.Writer;

            // Таким образом GetMemory() или GetSpan() — главные методы для записи. 
            Span<byte> res = pipeWriter.GetSpan();

            // принимает количество записанных байт. 
            pipeWriter.Advance(5);
            // И все тело метода является критической секцией и выполняется под локом.

            var task = pipeWriter.FlushAsync();
        }
    }
}
