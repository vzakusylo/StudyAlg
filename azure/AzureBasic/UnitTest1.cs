using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomTestValues;
using System;

namespace AzureBasic
{
    [TestClass]
    public class CreateQueue
    {
        [TestMethod]
        public void CreateQueueTest()
        {
            var name = "vadtest";// RandomValue.String(3);
            var namespaceManager = NamespaceManager.Create();
            if (!namespaceManager.QueueExists(name))
            {
                namespaceManager.CreateQueue(name);
            }
        }

        [TestMethod]
        public void SendQueueTest()
        {
            var name = "vadtest";
            var sendingClient = QueueClient.Create(name);

            object msgBody = RandomValue.String(3);
            sendingClient.Send(new BrokeredMessage(msgBody));

            Console.WriteLine("Message was sent");
        }

    }
}
