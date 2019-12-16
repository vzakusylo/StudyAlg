using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TemplateMethod
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
        }
    }

    //Шаблонный метод
    //на основе методов расширения
    public abstract class LogEntryBase
    {
        public DateTime EntryDateTime { get; set; }
        public Severity Severity { get; set; }
        public string Message { get; set; }

        public string AdditionalInformation { get; set; }
    }

    public static class LogEntryBaseEx
    {
        public static string GetText(this LogEntryBase logEntry)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("[{0}]", logEntry.EntryDateTime)
                .AppendFormat("[{0}]", logEntry.Severity)
                .AppendLine(logEntry.Message)
                .AppendLine(logEntry.AdditionalInformation);

            return sb.ToString();
        }
    }

    public class Severity
    {
    }

    //Локальный шаблонный метод на основе делегатов   
    public interface ILogSaver
    {
        void UploadLogEntries(IEnumerable<LogEntry> logEntries);
        void UploadExceptions(IEnumerable<ExceptionLogEntry> exceptions);
    }

    public class LogSaverProxy : ILogSaver
    {
        public void UploadLogEntries(IEnumerable<LogEntry> logEntries)
        {
            UseProxyClient(c => c.UploadLogEntries(logEntries));
        }

        public void UploadExceptions(IEnumerable<ExceptionLogEntry> exceptions)
        {
            UseProxyClient(c => c.UploadExceptions(exceptions));
        }
        // Использование наследования является слишком тяжеловесным решением, поэтому в таких случаях применяется подход, 
        // при котором переменный шаг алгоритма задается делегатом.

        private void UseProxyClient(Action<ILogSaver> accessor)
        {
            var client = new LogSaverClient();
            try
            {
                accessor(client.LogSaver);
                client.Close();
            }
            catch (CommunicationException e)
            {
                client.Abort();
                throw new OperationFailedException();
            }
        }
    }

    class LogSaverClient : ClientBase<ILogSaver>
    {
        private ILogSaver Channel;

        public ILogSaver LogSaver
        {
            get { return Channel; }
        }

        internal void Close()
        {
            throw new NotImplementedException();
        }

        internal void Abort()
        {
            throw new NotImplementedException();
        }
    }


    internal class OperationFailedException : Exception
    {
    }

    [Serializable]
    internal class CommunicationException : Exception
    {

    }


    class ClientBase<T>
    {
    }


    public class ExceptionLogEntry
    {
    }

    public class LogEntry
    {
    }
}


