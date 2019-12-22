﻿using Ordering.BuildingBlocks.EventBus.Events;
using System.Threading.Tasks;

namespace Ordering.BuildingBlocks.Abstractions
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
        where TIntegrationEvent : IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }

    public interface IIntegrationEventHandler {}
}
