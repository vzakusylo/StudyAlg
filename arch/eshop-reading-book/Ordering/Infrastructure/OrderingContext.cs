using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.SeedWork;
using Ordering.Infrastructure.EntityConfigurations;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Infrastructure
{
    public class OrderingContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;
        public const string DEFAULT_SCHEMA = "ordering";

        public OrderingContext([NotNullAttribute] DbContextOptions<OrderingContext> options) : base(options)
        {
        }

        public OrderingContext([NotNullAttribute] DbContextOptions<OrderingContext> options, 
            IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            System.Diagnostics.Debug.WriteLine($"OrderingContext::ctor => {this.GetHashCode()}");
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            // Dispatch Domain Events collection.
            // Choices:
            // A) Right BEFORE commiting data (EF SaveChanges) into the DB will make single 
            // transaction including side effects from the domain event handlers which are using 
            // the same DbContext with "InstancePerLifeScope" or "scoped" lifetime
            await _mediator.DispatchDomainEventsAsync(this);

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers)
            // performed throught the DbContext will be commited
            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
