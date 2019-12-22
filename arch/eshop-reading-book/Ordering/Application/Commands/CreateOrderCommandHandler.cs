using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.IntegrationEvents;
using Ordering.Domain.AggregatesModel.OrderAggregate;
using Ordering.Infrastructure.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IOrderingIntegrationEventService _orderingIntegrationEventService;
        private readonly IOrderRepository _orderRepository;
        private readonly IIdentityService _identityService;
        private readonly ILogger<CreateOrderCommandHandler> _logger;

        public CreateOrderCommandHandler(IMediator mediator,
            IOrderingIntegrationEventService orderingIntegrationEventService,
            IOrderRepository orderRepository,
            IIdentityService identityService,
            ILogger<CreateOrderCommandHandler> logger)
        {
            this._mediator = mediator;
            this._orderingIntegrationEventService = orderingIntegrationEventService;
            this._orderRepository = orderRepository ?? throw new ArgumentException(nameof(orderRepository));
            this._identityService = identityService;
            this._logger = logger;
        }
        public async Task<bool> Handle(CreateOrderCommand message, CancellationToken cancellationToken)
        {
            // Add integration event to clean the basket.
            var orderStartedIntegrationEvent = new OrderStartedIntegrationEvent(message.UserId);
            await _orderingIntegrationEventService.AddAndSaveEventAsync(orderStartedIntegrationEvent);

            // Add/Update the Buyer AggregateRoot
            // DDD patterns comment: Add child entitis and value objects throught the Order Aggregate-Root
            // methods and constructors so validations, invariants and business logic
            // make sure that consistency is preserved across the whole aggregate 
            var address = new Address(message.Street, message.City, message.State, message.Country, message.ZipCode);
            var order = new Order(message.UserId, message.UserName, address, message.CardTypeId, message.CardNumber,
                message.CardSecurityNumber, message.CardHolderName, message.CardExpiration);

            foreach (var item in message.OrderItems)
            {
                order.AddOrderItem(item.ProductId, item.ProductName,
                    item.UnitPrice, item.Discount, item.PictureUrl, item.Untis);
            }

            _logger.LogInformation("------ Creating Order - Order: {@Order}", order);
            _orderRepository.Add(order);

            return await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);          
        }
    }
}
