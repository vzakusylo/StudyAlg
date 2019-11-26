using Appointment.API.IntegrationEvents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Usavc.Services.Appointment.API.Infrastructure;
using Usavc.Services.Appointment.API.IntegrationEvents.Events;
using Usavc.Services.Appointment.API.Model;

namespace Usavc.Services.Appointment.API.Controllers
{
    [Route("api/v1/appointment")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly AppointmentContext _appointmentContext;
        private readonly AppointmentSettings _settings;
        private readonly IAppointmentIntegrationEventService _appointmentIntegrationEventService;

        public AppointmentController(AppointmentContext context, IOptionsSnapshot<AppointmentSettings> settings, IAppointmentIntegrationEventService appointmentIntegrationEventService)
        {
            _appointmentContext = context ?? throw new ArgumentNullException(nameof(context));
            _appointmentIntegrationEventService = appointmentIntegrationEventService ?? throw new ArgumentNullException(nameof(appointmentIntegrationEventService));
            _settings = settings.Value;

            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        [HttpGet]
        [Route("reasoncodes/{code}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ReasonCode), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ReasonCode>> ItemByIdAsync(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return BadRequest();
            }
            
            var item = await _appointmentContext.ReasonCodes.SingleOrDefaultAsync(ci => ci.Code == code);
            if (item != null)
            {
                return item;
            }

            return NotFound();
        }

        // GET api/v1/[controller]/reasoncodes
        [HttpGet]
        [Route("reasoncodes")]
        [ProducesResponseType(typeof(List<ReasonCode>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<ReasonCode>>> ReasonCodesAsync()
        {
            return await _appointmentContext.ReasonCodes.ToListAsync();
        }

        //PUT api/v1/[controller]/items
        [Route("reasoncodes")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> UpdateReasonCodeAsync([FromBody]ReasonCode reasonCodeToUpdate)
        {
            var reasonCode = await _appointmentContext.ReasonCodes.SingleOrDefaultAsync(i => i.Id == reasonCodeToUpdate.Id);

            if (reasonCode == null)
            {
                return NotFound(new { Message = $"Item with id {reasonCodeToUpdate.Id} not found." });
            }

            var oldName = reasonCode.Code;
            var raiseCodeChangedEvent = oldName != reasonCodeToUpdate.Code;

            // Update current reasonCode
            reasonCode = reasonCodeToUpdate;
            _appointmentContext.ReasonCodes.Update(reasonCode);

            if (raiseCodeChangedEvent) // Save reasonCode's data and publish integration event through the Event Bus if price has changed
            {
                //Create Integration Event to be published through the Event Bus
                var reasonCodeChangedEvent = new AppointmentReasonCodeChangedIntegrationEvent(reasonCode.Id, reasonCodeToUpdate.Code, oldName);

                // Achieving atomicity between original ReasonCode database operation and the IntegrationEventLog thanks to a local transaction
                await _appointmentIntegrationEventService.SaveEventAndReasonCodeContextChangesAsync(reasonCodeChangedEvent);

                // Publish through the Event Bus and mark the saved event as published
                await _appointmentIntegrationEventService.PublishThroughEventBusAsync(reasonCodeChangedEvent);
            }
            else // Just save the updated reason code because the ReasonCode cod hasn't changed.
            {
                await _appointmentContext.SaveChangesAsync();
            }

            return CreatedAtAction(nameof(ItemByIdAsync), new { id = reasonCodeToUpdate.Id }, null);
        }

        //POST api/v1/[controller]/items
        [Route("reasoncodes")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> CreateProductAsync([FromBody]ReasonCode reasonCode)
        {
            var item = new ReasonCode
            {
                Code = reasonCode.Code,
                Description = reasonCode.Description
                
            };

            _appointmentContext.ReasonCodes.Add(item);

            await _appointmentContext.SaveChangesAsync();

            return CreatedAtAction(nameof(ItemByIdAsync), new { id = item.Id }, null);
        }

        //DELETE api/v1/[controller]/code
        [Route("{code}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteProductAsync(string code)
        {
            var product = _appointmentContext.ReasonCodes.SingleOrDefault(x => x.Code == code);

            if (product == null)
            {
                return NotFound();
            }

            _appointmentContext.ReasonCodes.Remove(product);

            await _appointmentContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
