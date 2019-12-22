using Ordering.Application.Models;
using Ordering.BuildingBlocks.EventBus.Events;
using System;

namespace Ordering.Application.IntegrationEvents.Events
{
    public class UserCheckoutAcceptedIntegrationEvent : IntegrationEvent
    {
        public string UserId { get; }

        public CustomerBasket Basket {get;}
        public object City { get; internal set; }
        public object Street { get; internal set; }
        public object State { get; internal set; }
        public object Country { get; internal set; }
        public object ZipCode { get; internal set; }
        public object CardNumber { get; internal set; }
        public object CardHolderName { get; internal set; }
        public object CardExpiration { get; internal set; }
        public object CardSecurityNumber { get; internal set; }
        public object CardTypeId { get; internal set; }
        public Guid RequestId { get; internal set; }
    }
}
