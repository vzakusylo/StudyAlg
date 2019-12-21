using Ordering.BuildingBlocks.EventBus.Events;

namespace Ordering.Application.Commands
{
    internal class OrderStartedIntegrationEvent : IntegrationEvent
    {
        public string UserId;

        public OrderStartedIntegrationEvent(string userId) => UserId = userId;       
    }
}