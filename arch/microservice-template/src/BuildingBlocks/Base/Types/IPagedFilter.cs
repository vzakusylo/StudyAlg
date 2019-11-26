using System.Collections.Generic;
using Usavc.Common.Types;

namespace Usavc.Microservices.Common.Types
{
    public interface IPagedFilter<TResult, in TQuery> where TQuery : IQuery
    {
        PagedResult<TResult> Filter(IEnumerable<TResult> values, TQuery query);
    }
}