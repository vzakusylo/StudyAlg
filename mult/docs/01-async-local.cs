using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncLocal
{
    // Represents ambient data that is local to a given asynchronous control flow, 
    // such as an asynchronous method.
    [TestClass]
    public class Solution
    {
        static AsyncLocal<string> _asyncLocalString = new AsyncLocal<string>();
        static ThreadLocal<string> _threadLocalString = new ThreadLocal<string>();

        static async Task AsyncMethodA()
        {
            // Start multiple async method calls, with different AsyncLocal values.
            // We also set ThreadLocal values, to demonstrate how two mechanisms differ.
            _asyncLocalString.Value = "Value 1";
            _threadLocalString.Value = "Value 1";
            var t1 = AsyncMethodB("Value 1");

            _asyncLocalString.Value = "Value 2";
            _threadLocalString.Value = "Value 2";
            var t2 = AsyncMethodB("Value 2");

            await t1;
            await t2;
        }

        static async Task AsyncMethodB(string expectedValue)
        {
            Console.WriteLine("Entering AsyncMethodB");
            Console.WriteLine($"   Expected {expectedValue}, " +
                $"AsyncLocal value is '{_asyncLocalString.Value}', " +
                $"ThreadLocal value is '{_threadLocalString.Value}'");
            await Task.Delay(100);
            Console.WriteLine("Exiting AsyncMethodB");
            Console.WriteLine($"   Expected {expectedValue}, " +
                $"AsyncLocal value is '{_asyncLocalString.Value}', " +
                $"ThreadLocal value is '{_threadLocalString.Value}'");
        }

        [TestMethod]
        public async Task Main()
        {
            await AsyncMethodA();
            //Entering AsyncMethodB
            //   Expected Value 1, AsyncLocal value is 'Value 1', ThreadLocal value is 'Value 1'
            //Entering AsyncMethodB
            //   Expected Value 2, AsyncLocal value is 'Value 2', ThreadLocal value is 'Value 2'
            //Exiting AsyncMethodB
            //   Expected Value 2, AsyncLocal value is 'Value 2', ThreadLocal value is ''
            //Exiting AsyncMethodB
            //   Expected Value 1, AsyncLocal value is 'Value 1', ThreadLocal value is ''
        }
    }
}
