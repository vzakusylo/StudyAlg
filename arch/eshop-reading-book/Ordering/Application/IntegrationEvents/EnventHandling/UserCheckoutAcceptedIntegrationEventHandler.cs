using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Application.IntegrationEvents.Events;
using Ordering.BuildingBlocks.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Application.IntegrationEvents.EnventHandling
{
    public class UserCheckoutAcceptedIntegrationEventHandler : IIntegrationEventHandler<UserCheckoutAcceptedIntegrationEvent>
    {
        private readonly IMediator mediator;
        private readonly ILogger<UserCheckoutAcceptedIntegrationEventHandler> logger;

        public UserCheckoutAcceptedIntegrationEventHandler(
            IMediator mediator,
            ILogger<UserCheckoutAcceptedIntegrationEventHandler> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }
        public async Task Handle(UserCheckoutAcceptedIntegrationEvent evtMsg)
        {
            var result = false;
            if (evtMsg.RequestId != Guid.Empty)
            {
                var createOrderCommand = new CreateOrderCommand(
                    evtMsg.Basket.Items,
                    evtMsg.UserId,
                    evtMsg.City,
                    evtMsg.Street,
                    evtMsg.State,
                    evtMsg.Country,
                    evtMsg.ZipCode,
                    evtMsg.CardNumber,
                    evtMsg.CardHolderName,
                    evtMsg.CardExpiration,
                    evtMsg.CardSecurityNumber,
                    evtMsg.CardTypeId);

                var requestCreateOrder = new IdentifiedCommand<CreateOrderCommand, bool>(
                    createOrderCommand, evtMsg.RequestId);

                // logging

                result = await this.mediator.Send(requestCreateOrder);
                if (result)
                {
                    logger.LogInformation("---- Create order command suceeded - RequestId", evtMsg.RequestId);
                }
                else
                {
                    logger.LogWarning("CreateOrderCommand failed - RequestId", evtMsg.RequestId);
                }
            }
        }
    }
}
