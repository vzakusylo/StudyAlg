using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Infrastructure.Idempotency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ordering.Application.IntegrationEvents.Extentions;

namespace Ordering.Application.Commands
{
    public class IdentifiedCommandHandler<T, R> : IRequestHandler<IdentifiedCommand<T, R>, R> where T : IRequest<R>
    {
        private readonly IMediator mediator;
        private readonly IRequestManager requestManager;
        private readonly ILogger<IdentifiedCommand<T, R>> logger;

        public IdentifiedCommandHandler(
            IMediator mediator,
            IRequestManager requestManager,
            ILogger<IdentifiedCommand<T,R>> logger)
        {
            this.mediator = mediator;
            this.requestManager = requestManager;
            this.logger = logger;
        }
        public async Task<R> Handle(IdentifiedCommand<T, R> message, CancellationToken cancellationToken)
        {
            var alreadyExist =  await requestManager.ExistAsync(message.Id);
            if (alreadyExist)
            {
                return CreateResultForDuplicatedRequest();
            }
            else
            {
                await requestManager.CreateRequestForCommandAsync<T>(message.Id);
                try
                {
                    var command = message.Command;
                    var commandName = command.GetGenericTypeName();
                    var idProperty = string.Empty;
                    var commandId = string.Empty;
                    switch (command)
                    {
                        case CreateOrderCommand createOrderCommand:
                            idProperty = nameof(createOrderCommand.UserId);
                            commandId = createOrderCommand.UserId;
                            break;
                        case CancelOrderCommand cancelOrderCommand:
                            idProperty = nameof(cancelOrderCommand.OrderNumber);
                            commandId = cancelOrderCommand.OrderNumber;
                            break;
                        case ShipOrderCommand shipOrderCommand:
                            idProperty = nameof(shipOrderCommand.OrderNumber);
                            commandId = shipOrderCommand.OrderNumber;
                            break;
                        default:
                            idProperty = "Id?";
                            commandId = "n/a";
                            break;
                    }

                    logger.LogInformation("------ Sending command {CommandName} - {IdProperty}: {CommandId} ({@CommandId})", 
                        commandName,
                        idProperty,
                        commandId,
                        command);

                    var result = await mediator.Send(command, cancellationToken);

                    logger.LogInformation("----- Command result: {@Result} - {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                        result,
                        commandName,
                        idProperty,
                        commandId,
                        command);
                    return result;
                }
                catch 
                {

                    return default(R);
                }
            }
        }

        protected virtual R CreateResultForDuplicatedRequest()
        {
            return default(R);
        }
    }
}
