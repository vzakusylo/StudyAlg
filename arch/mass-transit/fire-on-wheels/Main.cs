using System;
using System.Threading.Tasks;
using fire_on_wheels.messaging;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace fire_on_wheels
{
    [TestClass]
    public class EntryPoint
    {
        [TestMethod]
        public async Task Main()
        {
            var taskProducer = Task.Factory.StartNew(async () => { 
                var bus = ConfigureBus();

                var sendToUrl = new Uri($"{RabbitMqConstants.RabbitMqUri}" +
                                        $"{RabbitMqConstants.RegisterOrderServiceQueue}" );
                var endPoint = await bus.GetSendEndpoint(sendToUrl);

                OrderViewModel model = new OrderViewModel()
                {
                    PickName = Guid.NewGuid().ToString(),
                    PickupAddress = Guid.NewGuid().ToString()
                };

                await endPoint.Send<IRegisterOrderCommand>(new {
                    PickupName = model.PickName,
                    PickupAddress = model.PickupAddress,
                });
            });

            var taskConsumer = Task.Factory.StartNew(() =>
             {
                 var busConsumer = ConfigureBus((cfg, host) =>
                 {
                     cfg.ReceiveEndpoint(host, RabbitMqConstants.RegisterOrderServiceQueue, e =>
                     {
                         e.Consumer<RegisterOrderCommandConsumer>();
                     });
                 });

                 busConsumer.Start();
                 Console.ReadLine();
                 busConsumer.Stop();
             });

            Task.WaitAll(taskConsumer, taskProducer);
        }

        public static IBusControl ConfigureBus(Action<IRabbitMqBusFactoryConfigurator, 
            IRabbitMqHost> registrationAction = null)
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(RabbitMqConstants.RabbitMqUri), 
                    hst =>  
                {
                    hst.Username(RabbitMqConstants.UserName);
                    hst.Password(RabbitMqConstants.Password);
                });

                registrationAction?.Invoke(cfg, host);
            });
        }
    }
}
