using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Usavc.Microservices.Appointment.Dto;
using Usavc.Microservices.Appointment.Messages.Commands;
using Usavc.Microservices.Appointment.Queries;
using Usavc.Microservices.Common.Dispatchers;

namespace Usavc.Microservices.Appointment.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public AppointmentController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        // Idempotent
        // No side effects
        // Doesn't mutate a state
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReasonCodeDto>>> Get([FromQuery] FindReasonCode query)
            => Ok(await _dispatcher.QueryAsync(query));

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ReasonCodeDto>> Get([FromRoute] GetReasonCode query)
        {
            var reasonCode = await _dispatcher.QueryAsync(query);
            if (reasonCode is null)
            {
                return NotFound();
            }

            return reasonCode;
        }

        [HttpPost]
        public async Task<ActionResult> Post(CreateReasonCode command)
        {
            await _dispatcher.SendAsync(command);

            return Accepted();
        }
    }
}