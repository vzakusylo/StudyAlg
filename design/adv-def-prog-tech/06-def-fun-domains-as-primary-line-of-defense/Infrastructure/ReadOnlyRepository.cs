using AutoMapper.QueryableExtensions;
using def_fun_domains_as_primary_line_of_defense;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adv_def_prog_tech._06_def_fun_domains_as_primary_line_of_defense.Infrastructure
{
    public class ReadOnlyRepository<TModel, TPesistance, TDbContext> : 
        IReadOnlyRepository<TModel> 
            where TPesistance : PesistentObject 
            where TDbContext : DbContext
    {
        private TDbContext DbContext { get; }
        private Func<TDbContext, DbSet<TPesistance>> GetDbSet { get; }

        private IQueryable<TPesistance> NonTrackingQuery => GetDbSet(DbContext).AsNoTracking<TPesistance>();

        //private IQueryable<TPesistance> NonTrackingModelQuery => NonTrackingQuery.ProjectTo<TModel>();

        public ReadOnlyRepository(Func<TDbContext> dbContextFactory, Func<TDbContext, DbSet<TPesistance>> getDbSet)
        {
            DbContext = dbContextFactory();
            GetDbSet = getDbSet;
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public TModel Find(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TModel> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
