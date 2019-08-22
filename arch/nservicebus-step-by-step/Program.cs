using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

namespace nservicebus_step_by_step
{
    internal class Program
    {
        private static readonly ILog log = LogManager.GetLogger<Program>();

        private static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.Title = "ClientUI";
            var endpointConfiguration = new EndpointConfiguration("ClientUI");
            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();

            await RunLoop(endpointInstance).ConfigureAwait(false);

            await endpointInstance.Stop().ConfigureAwait(false);
        }

        private static async Task RunLoop(IEndpointInstance endpointInstance)
        {
            while (true)
            {
                log.Info("Press 'P' to place an order, or 'Q' to quit.");
                var key = Console.ReadKey();
                Console.WriteLine();

                switch (key.Key)
                {
                    case ConsoleKey.P:

                        var command = new PlaceOrder
                        {
                            OrderId = Guid.NewGuid().ToString()
                        };
                        log.Info($"Sending PlaceOrder command, OrderId {command.OrderId}");
                        await endpointInstance.SendLocal(command).ConfigureAwait(false);
                        break;

                    case ConsoleKey.Q:
                        return;


                    default:
                        log.Info("Unknown input. Please try again.");
                        break;
                }
            }
        }

        public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
        {
            private static readonly ILog log = LogManager.GetLogger<PlaceOrderHandler>();

            public Task Handle(PlaceOrder message, IMessageHandlerContext context)
            {
                log.Info($"Received PlaceOrder, OrderId = {message.OrderId}");
                return Task.CompletedTask;
            }
        }

        public class PlaceOrder : ICommand
        {
            public string OrderId { get; set; }
        }

        public class DoSomethingHandler : IHandleMessages<DoSomething>
        {
            public Task Handle(DoSomething message, IMessageHandlerContext context)
            {
                return Task.CompletedTask;
            }
        }

        public class DoSomethingComplex : ICommand
        {
            public int SomeId { get; set; }
            public ChildClass ChildStuff { get; set; }
            public List<ChildClass> ListOfStuff { get; set; } = new List<ChildClass>();
        }

        public class DoSomething : ICommand
        {
            public string SomeProperty { get; set; }
        }
    }

    internal class ChildClass
    {
    }
}