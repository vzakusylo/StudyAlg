using System;
using System.Collections.Generic;
using System.Linq;

namespace Logger
{

    // - Abstract class (LogReader) - deffine non virtual method TEMPLATE METHOD (ReadLogEntry) 
    //   which calls internaly PrimitiveOperation1(), PrimitiveOperation2() - ReadEntries and ParseLogEntry
    // - ConcreteClass (LogFileReader) - implements primitive steps of algorithm.
    public abstract class LogReader
    {
        private int _currentPosition;

        public IEnumerable<LogEntry> ReadLogEntry() // this method is not virtual. It is define algorith of import
        {
            return ReadEntries(ref _currentPosition).Select(ParseLogEntry);
        }

        protected abstract IEnumerable<string> ReadEntries(ref int position);

        protected abstract LogEntry ParseLogEntry(string stringEntry);
    }

    public class LogProcessor
    {
        private readonly Func<List<LogEntry>> _logImporter;
        public LogProcessor(Func<List<LogEntry>> logImporter)
        {
            _logImporter = logImporter;
        }

        public void ProcessLogs()
        {
            foreach (var logEntry in _logImporter.Invoke())
            {
                SaveLogEntry(logEntry);
            }
        }

        private void SaveLogEntry(LogEntry logEntry)
        {
            throw new NotImplementedException();
        }
    }

    public class LogEntry
    {
    }
}
