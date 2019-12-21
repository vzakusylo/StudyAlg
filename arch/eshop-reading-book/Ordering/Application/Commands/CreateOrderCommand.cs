using MediatR;
using System;
using System.Collections.Generic;

namespace Ordering.Application.Commands
{
    public class CreateOrderCommand : IRequest<bool>
    {
        private readonly List<OrderItemDto> _orderItems;

        public string UserId { get; private set; }
        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }
        public string UserName { get; private set; }
        public int CardTypeId { get; private set; }
        public string CardNumber { get; private set; }
        public string CardSecurityNumber { get; private set; }
        public string CardHolderName { get; private set; }
        public DateTime CardExpiration { get; private set; }
        public IEnumerable<OrderItemDto> OrderItems => _orderItems;
    }

    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; internal set; }
        public decimal UnitPrice { get; internal set; }
        public decimal Discount { get; internal set; }
        public string PictureUrl { get; internal set; }
        public int Untis { get; internal set; }
    }
}