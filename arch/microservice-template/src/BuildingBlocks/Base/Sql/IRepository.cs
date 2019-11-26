using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Usavc.Common.Types;
using Usavc.Microservices.Common.Types;

namespace Usavc.Microservices.Common.Sql
{
    public interface IRepository<TEntity> where TEntity : IIdentifiable
    {
         Task<TEntity> GetAsync(string id);
         Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
         Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
         Task<PagedResult<TEntity>> BrowseAsync<TQuery>(Expression<Func<TEntity, bool>> predicate,
				TQuery query) where TQuery : PagedQueryBase;
         Task AddAsync(TEntity entity);
         Task UpdateAsync(TEntity entity);
         Task DeleteAsync(string id); 
         Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
    }
}