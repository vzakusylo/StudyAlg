using System.Threading.Tasks;
using Usavc.Common.Types;
using Usavc.Microservices.Common.Messages;

namespace Usavc.Microservices.Common.Dispatchers
{
    public interface IDispatcher
    {
        Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand;
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    }
}