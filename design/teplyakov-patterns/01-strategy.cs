using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Page33
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            var list = new List<Employee>();
            list.Sort(new EmployeeByIdComparer()); //using functor

            list.Sort((x, y) => x.Name.CompareTo(y.Name));// using delegate

            var comparer = new EmployeeByIdComparer();
            var set = new SortedSet<Employee>(comparer);

            var comparer1 = ComparerFactory.Create<Employee>((x, y) => x.Id.CompareTo(y.Id));
            var set1 = new SortedSet<Employee>(comparer1);
        }
    }

    class ComparerFactory
    {
        public static IComparer<T> Create<T>(Comparison<T> comparer)
        {
            return new DelegateComparer<T>(comparer);
        }

        private class DelegateComparer<T> : IComparer<T>
        {
            private readonly Comparison<T> _comparer;

            public DelegateComparer(Comparison<T> comparer)
            {
                _comparer = comparer;
            }

            public int Compare(T x, T y)
            {
                return _comparer(x, y);
            }
        }
    }

    class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return $"id:{Id} name:{Name}";
        }
    }

    class EmployeeByIdComparer : IComparer<Employee>
    {
        public int Compare([AllowNull] Employee x, [AllowNull] Employee y)
        {
            return x.Id.CompareTo(y.Id);
        }
    }
}

namespace Strategy
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
        }
    }

    class LogProcessor
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

     class LogEntry
    {
    }
}
