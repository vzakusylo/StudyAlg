namespace Usavc.Services.Appointment.API.IntegrationEvents.Events
{
    using BuildingBlocks.EventBus.Events;
    using System.Collections.Generic;

    public class ReasonCodeRejectedIntegrationEvent : IntegrationEvent
    {
        public int BrandId { get; }

        public ReasonCodeRejectedIntegrationEvent(int brandId)
        {
            BrandId = brandId;
        }
    }
}