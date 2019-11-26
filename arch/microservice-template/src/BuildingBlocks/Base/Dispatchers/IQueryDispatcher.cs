using System.Threading.Tasks;
using Usavc.Common.Types;

namespace Usavc.Microservices.Common.Dispatchers
{
    public interface IQueryDispatcher
    {
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    }
}