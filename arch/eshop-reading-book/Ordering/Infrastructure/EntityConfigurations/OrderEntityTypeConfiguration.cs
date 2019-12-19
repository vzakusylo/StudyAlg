using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Ordering.Domain.AggregatesModel.OrderAggregate;

namespace Ordering.Infrastructure.EntityConfigurations
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> orderConfiguration)
        {
            orderConfiguration.ToTable("orders", OrderingContext.DEFAULT_SCHEMA);
            orderConfiguration.HasKey(o => o.Id);
            orderConfiguration.Ignore(b => b.DomainEvent);         
            orderConfiguration.Property(o => o.Id).UseHiLo("orderseq", OrderingContext.DEFAULT_SCHEMA);
            orderConfiguration.OwnsOne(o => o.Address, a => { a.WithOwner(); });
            //В приведенном выше коде метод orderConfiguration.OwnsOne(o => o.Address) указывает, что свойство
            //Address принадлежит сущности типа Order.

        }
    }
}
