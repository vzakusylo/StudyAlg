using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Observer
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            LogFileReader reader = new LogFileReader("1.txt");
            reader.OnNewLogEntry += Reader_OnNewLogEntry;
            
        }

        private void Reader_OnNewLogEntry(object sender, LogEntryEventArgs e)
        {
            throw new NotImplementedException();
        }
    }


    // observer using sharp event
    public class LogFileReader : IDisposable
    {
        public LogFileReader(string logFileName)
        {

        }

        public event EventHandler<LogEntryEventArgs> OnNewLogEntry;

        private void CheckFile()
        {
            foreach (var item in ReadLogEntries())
            {
                RaiseNewLogEntry(item);
            }
        }

        private void RaiseNewLogEntry(string logEntry)
        {
            var handler = OnNewLogEntry;
            if (handler != null)
            {
                handler(this, new LogEntryEventArgs(logEntry));
            }
        }

        private IEnumerable<string> ReadLogEntries()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    public class LogEntryEventArgs : EventArgs
    {
        public LogEntryEventArgs(string logEntry)
        {
            LogEntry = logEntry;
        }

        public string LogEntry { get; private set; }
    }
}
