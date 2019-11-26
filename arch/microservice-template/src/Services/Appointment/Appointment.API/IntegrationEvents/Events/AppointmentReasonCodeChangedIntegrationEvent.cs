namespace Usavc.Services.Appointment.API.IntegrationEvents.Events
{
    using BuildingBlocks.EventBus.Events;

    // Integration Events notes: 
    // An Event is “something that has happened in the past”, therefore its name has to be   
    // An Integration Event is an event that can cause side effects to other microsrvices, Bounded-Contexts or external systems.
    public class AppointmentReasonCodeChangedIntegrationEvent : IntegrationEvent
    {        
        public int BrandId { get; private set; }

        public string NewName { get; private set; }

        public string OldName { get; private set; }

        public AppointmentReasonCodeChangedIntegrationEvent(int brandId, string newName, string oldName)
        {
            BrandId = brandId;
            NewName = newName;
            OldName = oldName;
        }
    }
}