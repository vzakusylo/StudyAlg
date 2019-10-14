using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mult_any
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task PotentialDeadLock()
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync("http://slowwly.robertomurray.co.uk/delay/3000/url/http://www.google.co.uk");
                string download = await result.Content.ReadAsStringAsync();
            }
        }
    }
}
