using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nito.AsyncEx;
using System;
using System.Threading.Tasks;

namespace async_ex
{
    [TestClass]
    public class Solution
    {
        private readonly AsyncLock _mutex = new AsyncLock();       
        public async Task UseLockAsync()
        {
            using ( await _mutex.LockAsync())
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
    }
}
