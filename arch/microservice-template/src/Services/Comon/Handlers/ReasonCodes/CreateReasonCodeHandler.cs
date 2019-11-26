using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Usavc.Common.Types;
using Usavc.Microservices.Appointment.Domain;
using Usavc.Microservices.Appointment.Messages.Commands;
using Usavc.Microservices.Appointment.Repositories;
using Usavc.Microservices.Common.Handlers;
using Usavc.Microservices.Common.RabbitMq;
using Usavc.Microservices.Common.Types;
using Usavc.Services.Discounts.Repositories;

namespace Usavc.Microservices.Appointment.Handlers.ReasonCodes
{
    public class CreateReasonCodeHandler : ICommandHandler<CreateReasonCode>
    {
        private readonly IReasonCodeRepository _reasonCodesRepository;
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<CreateReasonCodeHandler> _logger;

        public CreateReasonCodeHandler(IReasonCodeRepository reasonCodesRepository,
            IReasonCodeRepository reasonCodeRepository,
            IBusPublisher busPublisher, ILogger<CreateReasonCodeHandler> logger)
        {
            _reasonCodesRepository = reasonCodesRepository;
            _busPublisher = busPublisher;
            _logger = logger;
        }
        
        // Command -> Event - how to track this simple use case?
        // Command -> Event -> Event -> Command -> Event ... or this one?

        public async Task HandleAsync(CreateReasonCode command, ICorrelationContext context)
        {
            // ReasonCode validation
            var rc = await _reasonCodesRepository.GetAsync(command.Code);
            if (rc != null)
            {
                //onError: -> publish CreateReasonCodeRejected
                throw new UsavcException("reason_code_already_exists",
                    $"ReasonCode : '{command.Code}' exists.");
            }

            // Unique code validation
            var newReasonCode = new ReasonCode(command.Code, command.Description);
            await _reasonCodesRepository.AddAsync(newReasonCode);
            var @event = new ReasonCodeCreated(newReasonCode.Code, newReasonCode.Description);
            await _busPublisher.PublishAsync(@event, context);
            // Send an email about a new discount to the customer
        }
    }
}