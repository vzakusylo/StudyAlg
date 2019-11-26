using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Usavc.Common.Types;
using Usavc.Microservices.Common.Types;

namespace Usavc.Microservices.Common.Sql
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : IIdentifiable
    {
        protected ICollection<TEntity> Collection { get; }

		public Repository(IDatabase database, string collectionName)
		{
			Collection = database.GetCollection<TEntity>(collectionName);
		}

        public async Task<TEntity> GetAsync(string id)
            => await GetAsync(e => e.Id == id);

        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<TEntity>> BrowseAsync<TQuery>(Expression<Func<TEntity, bool>> predicate, TQuery query) where TQuery : PagedQueryBase
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}