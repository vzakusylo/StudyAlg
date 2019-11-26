using System.Threading.Tasks;
using Usavc.Microservices.Common.Messages;

namespace Usavc.Microservices.Common.Dispatchers
{
    public interface ICommandDispatcher
    {
         Task SendAsync<T>(T command) where T : ICommand;
    }
}