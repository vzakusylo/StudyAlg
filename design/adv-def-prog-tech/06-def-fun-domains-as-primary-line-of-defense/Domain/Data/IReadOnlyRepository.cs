using System;
using System.Linq;

namespace def_fun_domains_as_primary_line_of_defense
{
    public interface IReadOnlyRepository<TModel> : IDisposable
    {
        IQueryable<TModel> GetAll();
        TModel Find(int id); 
    }
}
