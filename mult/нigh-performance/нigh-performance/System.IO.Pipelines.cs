using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
