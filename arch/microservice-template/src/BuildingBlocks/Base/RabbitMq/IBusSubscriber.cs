using System;
using Usavc.Common.Types;
using Usavc.Microservices.Common.Messages;
using Usavc.Microservices.Common.Types;

namespace Usavc.Microservices.Common.RabbitMq
{
    public interface IBusSubscriber
    {
        IBusSubscriber SubscribeCommand<TCommand>(string @namespace = null, string queueName = null,
            Func<TCommand, UsavcException, IRejectedEvent> onError = null)
            where TCommand : ICommand;

        IBusSubscriber SubscribeEvent<TEvent>(string @namespace = null, string queueName = null,
            Func<TEvent, UsavcException, IRejectedEvent> onError = null) 
            where TEvent : IEvent;
    }
}
