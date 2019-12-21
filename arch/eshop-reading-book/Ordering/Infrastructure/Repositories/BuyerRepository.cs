using Ordering.Domain.AggregatesModel.BuyerAggregate;
using Ordering.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class BuyerRepository : IBuyerRepository
    {
        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public Buyer Add(Buyer buyer)
        {
            throw new NotImplementedException();
        }

        public Task<Buyer> FindAsync()
        {
            throw new NotImplementedException();
        }

        public Buyer Update(Buyer buyer)
        {
            throw new NotImplementedException();
        }
    }
}
