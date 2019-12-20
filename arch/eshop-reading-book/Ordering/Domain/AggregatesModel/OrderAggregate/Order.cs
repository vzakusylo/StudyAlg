using Ordering.Domain.Events;
using Ordering.Domain.SeedWork;
using System;

namespace Ordering.Domain.AggregatesModel.OrderAggregate
{
    public class Order 
        : Entity, IAggregateRoot
    {
        // Using private fields, allowed since Core 1.1 is a much better encapsulation
        // aligned with DDD Aggregates and Domain Entities (instead of properties and property collections)
        private DateTime _orderDate;
        public int? GetBuyerId => _buyerId;
        private int? _buyerId;

        private int? _paymentMethodId;
        private int _orderStatusId;

        public Address Address { get; internal set; }

        public OrderStatus OrderStatus { get; private set; }

        public Order(string userId, string userName, Address address, int cardTypeId, string cardNumber,
            string cardSecurityNumber, string cardHolderName, DateTime cardExpiration,
            int? buyerId = null, int? paymentMethod = null)
        {
            _buyerId = buyerId;
            _paymentMethodId = paymentMethod;
            _orderStatusId = OrderStatus.Submited.Id;
            _orderDate = DateTime.UtcNow;
            Address = address;

            AddOrderStartedDomainEvent(userId, userName, cardTypeId, cardNumber,
                cardSecurityNumber, cardHolderName, cardExpiration);
        }

        private void AddOrderStartedDomainEvent(string userId, string userName, int cardTypeId, 
            string cardNumber, string cardSecurityNumber, string cardHolderName, 
            DateTime cardExpiration)
        {
            var orderStartedDomainEvent = new OrderStartedDomainEvent(this, userId, userName,
                cardTypeId, cardNumber, cardSecurityNumber, cardHolderName, cardExpiration);

            AddDomainEvent(orderStartedDomainEvent);
        }
    }
}
