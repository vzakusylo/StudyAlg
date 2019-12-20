using Ordering.Domain.SeedWork;
using System;

namespace Ordering.Domain.AggregatesModel.OrderAggregate
{
    public class OrderStatus : Enumeration
    {
        public static OrderStatus Submited = new OrderStatus(1, nameof(Submited).ToLowerInvariant());

        public OrderStatus(int id, string name) : base(id, name)
        {

        }
    }
}