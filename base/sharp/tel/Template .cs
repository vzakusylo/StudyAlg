using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day_1301
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
        }

        async Task<HttpResponseMessage> GetWithTimeoutAsync(HttpClient client, string url, CancellationToken ct)
        {
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(ct);
            cts.CancelAfter(TimeSpan.FromSeconds(2));

            var combined = cts.Token;

            return await client.GetAsync(url, combined);
        }
    }
}
