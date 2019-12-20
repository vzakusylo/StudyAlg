using Autofac;
using MediatR;
using Ordering.Application.DomainEventHandlers;
using System.Reflection;

namespace Ordering.Infrastructure.AutofacModules
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly)
                .AsImplementedInterfaces();

            // Register the DomainEventHandler classes (they implement INotifiactionHandler<>) in assembly holding the Domain Events
            builder.RegisterAssemblyTypes(typeof(ValidateOrAddBuyerAggregateWhenOrderStartedDomainEventHandler)
                .GetTypeInfo().Assembly).AsClosedTypesOf(typeof(INotificationHandler<>));

            base.Load(builder);
        }
    }
}
