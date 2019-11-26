namespace Usavc.Services.Appointment.API.IntegrationEvents.Events
{
    using BuildingBlocks.EventBus.Events;

    public class ReasonCodeConfirmedIntegrationEvent : IntegrationEvent
    {
        public int BrandId { get; }

        public ReasonCodeConfirmedIntegrationEvent(int orderId) => BrandId = orderId;
    }
}