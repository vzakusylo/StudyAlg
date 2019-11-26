using System.Threading.Tasks;
using Autofac;
using Usavc.Microservices.Common.Handlers;
using Usavc.Microservices.Common.Messages;
using Usavc.Microservices.Common.RabbitMq;

namespace Usavc.Microservices.Common.Dispatchers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext _context;

        public CommandDispatcher(IComponentContext context)
        {
            _context = context;
        }

        public async Task SendAsync<T>(T command) where T : ICommand
            => await _context.Resolve<ICommandHandler<T>>().HandleAsync(command, CorrelationContext.Empty);
    }
}