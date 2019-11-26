using Microsoft.Extensions.Logging;

namespace Usavc.Services.Appointment.API.IntegrationEvents.EventHandling
{
    using BuildingBlocks.EventBus.Abstractions;
    using System.Threading.Tasks;
    using BuildingBlocks.EventBus.Events;
    using Infrastructure;
    using global::Appointment.API.IntegrationEvents;
    using Events;
    using Serilog.Context;

    public class AppointmentReasonCodeChangedToAwaitingValidationIntegrationEventHandler : 
        IIntegrationEventHandler<AppointmentReasonCodeChangedIntegrationEvent>
    {
        private readonly AppointmentContext _appointmentContext;
        private readonly IAppointmentIntegrationEventService _appointmentIntegrationEventService;
        private readonly ILogger<AppointmentReasonCodeChangedToAwaitingValidationIntegrationEventHandler> _logger;

        public AppointmentReasonCodeChangedToAwaitingValidationIntegrationEventHandler(
            AppointmentContext appointmentContext,
            IAppointmentIntegrationEventService appointmentIntegrationEventService,
            ILogger<AppointmentReasonCodeChangedToAwaitingValidationIntegrationEventHandler> logger)
        {
            _appointmentContext = appointmentContext;
            _appointmentIntegrationEventService = appointmentIntegrationEventService;
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public async Task Handle(AppointmentReasonCodeChangedIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
            {
                _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);
                
                var confirmedIntegrationEvent = !@event.NewName.Contains("microservice")
                    ? (IntegrationEvent)new ReasonCodeRejectedIntegrationEvent(@event.BrandId)
                    : new ReasonCodeConfirmedIntegrationEvent(@event.BrandId);

                await _appointmentIntegrationEventService.SaveEventAndReasonCodeContextChangesAsync(confirmedIntegrationEvent);
                await _appointmentIntegrationEventService.PublishThroughEventBusAsync(confirmedIntegrationEvent);

            }
        }
    }
}