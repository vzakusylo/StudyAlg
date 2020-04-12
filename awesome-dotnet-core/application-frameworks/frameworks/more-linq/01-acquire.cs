using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static MoreLinq.Extensions.AcquireExtension;

namespace more_linq_acquire
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            // Ensures that a source sequence of disposable objects are all acquired successfully. 
            // If the acquisition of any one fails then those successfully acquired till that point are disposed.

            GetObjects().Acquire();
        }

        public IEnumerable<Disposible> GetObjects()
        {
            yield return new Disposible(1);
            yield return new Disposible(2);
            yield return new Disposible(3);
        }
    }

    public class Disposible : IDisposable
    {
        private readonly int val;
        bool disposed = false;

        public Disposible(int val)
        {
            this.val = val;
            Console.WriteLine($"Created {val}");
        }

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            System.Diagnostics.Debugger.Launch();
            Console.WriteLine($"Dispose {val}");
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            Console.WriteLine($"Dispose {val}");
            if (disposed)
                return;

            if (disposing)
            {
                Console.WriteLine($"disposing {val}");
            }

            disposed = true;
        }
              
    }
}
