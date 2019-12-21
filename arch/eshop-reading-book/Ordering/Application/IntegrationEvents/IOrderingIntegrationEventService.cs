using Ordering.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Application.IntegrationEvents
{
    public interface IOrderingIntegrationEventService
    {
        Task PublishEventsThroughtBusAsync(Guid transactionId);
        Task AddAndSaveEventAsync(IntegrationEvent ent);
    }
}
