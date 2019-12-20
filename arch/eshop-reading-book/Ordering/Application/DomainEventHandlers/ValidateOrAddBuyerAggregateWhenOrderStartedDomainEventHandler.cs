using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.IntegrationEvents;
using Ordering.Application.IntegrationEvents.Events;
using Ordering.Domain.AggregatesModel.BuyerAggregate;
using Ordering.Domain.Events;
using Ordering.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.DomainEventHandlers
{
    public class ValidateOrAddBuyerAggregateWhenOrderStartedDomainEventHandler : INotificationHandler<OrderStartedDomainEvent>
    {
        private readonly IIdentityService _identityService;
        private readonly IOrderIntegrationEventService _orderIntegrationEventService;
        private readonly ILoggerFactory _logger;
        private readonly IBuyerRepository _buyerRepository;

        public ValidateOrAddBuyerAggregateWhenOrderStartedDomainEventHandler(
            ILoggerFactory logger,
            IBuyerRepository buyerRepository,
            IIdentityService identityService,
            IOrderIntegrationEventService orderIntegrationEventService)
        {
            this._identityService = identityService;
            this._orderIntegrationEventService = orderIntegrationEventService;
            this._logger = logger;
            this._buyerRepository = buyerRepository;
        }

        public async Task Handle(OrderStartedDomainEvent notification, CancellationToken cancellationToken)
        {
            var cardTypeId = (notification.CardTypeId != 0) ? notification.CardTypeId : 1;
            var userGuid = _identityService.GetUserIdentity();
            var buyer = await _buyerRepository.FindAsync();
            bool buyerOriginallyExisted = (buyer == null) ? false : true;

            if (!buyerOriginallyExisted)
            {
                buyer = new Buyer(userGuid);
            }

            buyer.VerifyOrAddPaymentMethod(cardTypeId,
                $"Payment Method on {DateTime.UtcNow}",
                notification.CardNumber,
                notification.CardSecurityNumber,
                notification.CardHolderName,
                notification.CardExpiration,
                notification.Order.Id);

            var buyerUpdated = buyerOriginallyExisted ?
                _buyerRepository.Update(buyer) :
                _buyerRepository.Add(buyer);

            await _buyerRepository.UnitOfWork
                .SaveChangesAsync(cancellationToken);

            var orderStatusChangedToSubmittedIntegrationEvent = new OrderStatusChangedToSubmittedIntegrationEvent(
                notification.Order.Id, notification.Order.OrderStatus.Name, buyer.Name);

            await _orderIntegrationEventService.AddAndSaveEventAsync(orderStatusChangedToSubmittedIntegrationEvent);

            _logger.CreateLogger<ValidateOrAddBuyerAggregateWhenOrderStartedDomainEventHandler>()
                .LogTrace($"Buyer {buyerUpdated.Id} and related payment method were validated or updated " +
                $"for order id {notification.Order.Id}");
        }
    }
}
