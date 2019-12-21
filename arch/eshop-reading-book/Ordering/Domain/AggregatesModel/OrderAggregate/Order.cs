using Ordering.Domain.Events;
using Ordering.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ordering.Domain.AggregatesModel.OrderAggregate
{
    public class Order 
        : Entity, IAggregateRoot
    {
        // Using private fields, allowed since Core 1.1 is a much better encapsulation
        // aligned with DDD Aggregates and Domain Entities (instead of properties and property collections)
        private DateTime _orderDate;
        private int? _buyerId;
        private int? _paymentMethodId;
        private int _orderStatusId;
        private string userId;
        private string userName;
        private int cardTypeId;
        private string cardNumber;
        private string cardSecurityNumber;
        private readonly List<OrderItem> _orderItems;

        public int? GetBuyerId => _buyerId;
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

        public Order(string userId, string userName, Address address, int cardTypeId, 
            string cardNumber, string cardSecurityNumber)
        {
            this.userId = userId;
            this.userName = userName;
            Address = address;
            this.cardTypeId = cardTypeId;
            this.cardNumber = cardNumber;
            this.cardSecurityNumber = cardSecurityNumber;
        }

        // DDD Patterns comment
        // This Order AggregateRoot's method "AddOrderItem" should be the only why to add Itmes to the Order,
        // (discounts, etc) and validation are controlled by the AggregateRoot
        // in order to maintain consistncy between the whole Aggregate.
        public void AddOrderItem(int productId, string productName, decimal unitPrice, decimal discount, 
            string pictureUrl, int units = 1)
        {
            var existingOrderForProduct = _orderItems.Where(o => o.ProductId == productId)
                .SingleOrDefault();
            if (existingOrderForProduct != null)
            {
                // if previous line exist modify it with higher discount and units..
                if (discount > existingOrderForProduct.GetCurrentDiscount())
                {
                    existingOrderForProduct.SetNewDiscount(discount);
                }
                existingOrderForProduct.AddUnits(units);
            }
            else
            {
                // add validated new order item
                var orderItem = new OrderItem(productId, productName, unitPrice, discount, pictureUrl, units);
                _orderItems.Add(orderItem);
            }            
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
