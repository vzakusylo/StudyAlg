using Microsoft.Azure.EventHubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace basic
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var ehConnStringBuilder = new EventHubsConnectionStringBuilder("con str from portal");
            ehConnStringBuilder.TransportType = TransportType.AmqpWebSockets;
            var ehClient2 = EventHubClient.CreateFromConnectionString(ehConnStringBuilder.ToString());

            ehClient2.WebProxy = new System.Net.WebProxy("https://myproxyserver");
        }
    }
}
