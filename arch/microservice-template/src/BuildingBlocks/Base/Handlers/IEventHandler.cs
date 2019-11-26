using System.Threading.Tasks;
using Usavc.Microservices.Common.Messages;
using Usavc.Microservices.Common.RabbitMq;

namespace Usavc.Microservices.Common.Handlers
{
    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent @event, ICorrelationContext context);
    }
}