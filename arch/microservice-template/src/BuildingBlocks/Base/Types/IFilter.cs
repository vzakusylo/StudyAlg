using System.Collections.Generic;
using Usavc.Common.Types;

namespace Usavc.Microservices.Common.Types
{
    public interface IFilter<TResult, in TQuery> where TQuery : IQuery
    {
        IEnumerable<TResult> Filter(IEnumerable<TResult> values, TQuery query);
    }
}