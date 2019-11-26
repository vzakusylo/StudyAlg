using System.Collections.Generic;
using Usavc.Microservices.Common.Types;

namespace Usavc.Microservices.Common.Sql
{
    public interface IDatabase
    {
        ICollection<TEntity> GetCollection<TEntity>(string collectionName) where TEntity : IIdentifiable;
    }
}