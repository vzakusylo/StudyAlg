using Ordering.Domain.AggregatesModel.OrderAggregate;
using Ordering.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public void Add(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
