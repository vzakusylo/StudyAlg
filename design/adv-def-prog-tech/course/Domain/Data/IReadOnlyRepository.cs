using System;
using System.Linq;

namespace Course
{
    public interface IReadOnlyRepository<TModel> : IDisposable
    {
        IQueryable<TModel> GetAll();
        TModel Find(int id); 
    }
}
