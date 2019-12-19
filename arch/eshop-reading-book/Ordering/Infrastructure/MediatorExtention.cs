using MediatR;
using Ordering.Domain.SeedWork;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Infrastructure
{
    public static class MediatorExtention
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, OrderingContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvent != null && x.Entity.DomainEvent.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvent)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
            }
        }
    }
}
