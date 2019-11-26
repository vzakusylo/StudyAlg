using Usavc.BuildingBlocks.EventBus.Events;
using System.Threading.Tasks;

namespace Appointment.API.IntegrationEvents
{
    public interface IAppointmentIntegrationEventService
    {
        Task SaveEventAndReasonCodeContextChangesAsync(IntegrationEvent evt);
        Task PublishThroughEventBusAsync(IntegrationEvent evt);
    }
}
