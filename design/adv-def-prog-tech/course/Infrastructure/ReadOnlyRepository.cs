using AutoMapper.QueryableExtensions;
using Course;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Course.Infrastructure
{

    public class MappingRepository<TModel, TPesistance, TDbContext> : IRepository<TModel>
        where TPesistance : class
        where TDbContext : DbContext
    {
        private TDbContext DbContext { get; }
        private IModelConverter<TModel, TPesistance> Converter { get; }
        private Func<TDbContext, DbSet<TPesistance>> GetDbSet { get; }

        private IDictionary<TModel, TPesistance> MaterializedObjects { get; }
        private DbSet<TPesistance> DbSet => GetDbSet(DbContext);

        public MappingRepository(Func<TDbContext> dbContextFactory, 
            IModelConverter<TModel, TPesistance> converter,
            Func<TDbContext, DbSet<TPesistance>> getDbSet)
        {
            DbContext = dbContextFactory();
            Converter = converter;
            MaterializedObjects = new Dictionary<TModel, TPesistance>();
            GetDbSet = getDbSet;
        }

        public TModel Find(int id)
        {
            EnsureNotDisposed();

            TPesistance persised = DbSet.Find(id);
            TModel model = Converter.ToModel(persised);
            MaterializedObjects[model] = persised;

            return model;
        }

        public void Add(TModel obj)
        {
            EnsureNotDisposed();
            if (obj == null)
            {
                throw new ArgumentException();
            }

            TPesistance persisted = Converter.ToPesisted(obj);
            DbSet.Add(persisted);
            MaterializedObjects[obj] = persisted;
        }


        public void Delete(TModel obj)
        {
            EnsureNotDisposed();
            if (obj != null && !MaterializedObjects.ContainsKey(obj))
            {
                throw new ArgumentException();
            }

            TPesistance persisted = MaterializedObjects[obj];
            DbSet.Remove(persisted);
            MaterializedObjects.Remove(obj);
        }

        public void SaveChanges()
        {
            EnsureNotDisposed();

            foreach (var item in MaterializedObjects)
            {
                Converter.CopyChanges(item.Key, item.Value);
            }
            DbContext.SaveChanges();
        }


        private void EnsureNotDisposed()
        {
            throw new NotImplementedException();
        }
    }

    public class ReadOnlyRepository<TModel, TPesistance, TDbContext> : 
        IReadOnlyRepository<TModel> 
            where TPesistance : PersistentObject 
            where TDbContext : DbContext
    {
        private TDbContext DbContext { get; }
        private Func<TDbContext, DbSet<TPesistance>> GetDbSet { get; }

        private IQueryable<TPesistance> NonTrackingQuery => GetDbSet(DbContext).AsNoTracking<TPesistance>();

       // private IQueryable<TPesistance> NonTrackingModelQuery => NonTrackingQuery.ProjectTo<TModel>(null, null, null);

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
