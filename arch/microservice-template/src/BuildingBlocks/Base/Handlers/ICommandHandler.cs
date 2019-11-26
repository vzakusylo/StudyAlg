using System.Threading.Tasks;
using Usavc.Microservices.Common.Messages;
using Usavc.Microservices.Common.RabbitMq;

namespace Usavc.Microservices.Common.Handlers
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command, ICorrelationContext context);
    }
}