using System.Threading.Tasks;
using Usavc.Common.Types;

namespace Usavc.Common.Handlers
{
    public interface IQueryHandler<TQuery,TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}