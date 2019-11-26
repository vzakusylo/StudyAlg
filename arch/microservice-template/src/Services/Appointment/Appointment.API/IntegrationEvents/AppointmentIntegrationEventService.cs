using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Usavc.BuildingBlocks.EventBus.Abstractions;
using Usavc.BuildingBlocks.EventBus.Events;
using Usavc.BuildingBlocks.IntegrationEventLogEF.Services;
using Usavc.BuildingBlocks.IntegrationEventLogEF.Utilities;
using Usavc.Services.Appointment.API;
using Usavc.Services.Appointment.API.Infrastructure;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace Appointment.API.IntegrationEvents
{
    public class AppointmentIntegrationEventService : IAppointmentIntegrationEventService
    {
        private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly IEventBus _eventBus;
        private readonly AppointmentContext _appointmentContext;
        private readonly IIntegrationEventLogService _eventLogService;
        private readonly ILogger<AppointmentIntegrationEventService> _logger;

        public AppointmentIntegrationEventService(
            ILogger<AppointmentIntegrationEventService> logger,
            IEventBus eventBus,
            AppointmentContext appointmentContext,
            Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _appointmentContext = appointmentContext ?? throw new ArgumentNullException(nameof(appointmentContext));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventLogService = _integrationEventLogServiceFactory(_appointmentContext.Database.GetDbConnection());
        }

        public async Task PublishThroughEventBusAsync(IntegrationEvent evt)
        {
            try
            {
                _logger.LogInformation("----- Publishing integration event: {IntegrationEventId_published} from {AppName} - ({@IntegrationEvent})", evt.Id, Program.AppName, evt);

                await _eventLogService.MarkEventAsInProgressAsync(evt.Id);
                _eventBus.Publish(evt);
                await _eventLogService.MarkEventAsPublishedAsync(evt.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})", evt.Id, Program.AppName, evt);
                await _eventLogService.MarkEventAsFailedAsync(evt.Id);
            }
        }

        public async Task SaveEventAndReasonCodeContextChangesAsync(IntegrationEvent evt)
        {
            _logger.LogInformation("----- AppointmentIntegrationEventService - Saving changes and integrationEvent: {IntegrationEventId}", evt.Id);

            //Use of an EF Core resiliency strategy when using multiple DbContexts within an explicit BeginTransaction():
            //See: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency            
            await ResilientTransaction.New(_appointmentContext).ExecuteAsync(async () =>
            {
                // Achieving atomicity between original catalog database operation and the IntegrationEventLog thanks to a local transaction
                await _appointmentContext.SaveChangesAsync();
                await _eventLogService.SaveEventAsync(evt, _appointmentContext.Database.CurrentTransaction);
            });
        }
    }
}
