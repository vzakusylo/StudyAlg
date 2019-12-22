using MediatR;
using Ordering.Application.Models;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Ordering.Application.Commands
{
    // команды — это попросту структуры
    // данных, которыесодержат доступныетолько для чтения данные, но неалгоритмы.Имя команды указывает
    // наее назначение.

    // DDD and CQRS patterns comment: Note that it is recommended to implement immutable Commands
    // In this case, its immutability is achieved by having all the setters as private
    // plus only being able to update the data just once, when creating the object through its constructor.
    // References on Immutable Commands:  
    // http://cqrs.nu/Faq
    // https://docs.spine3.org/motivation/immutability.html 
    // http://blog.gauffin.org/2012/06/griffin-container-introducing-command-support/
    // https://docs.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/how-to-implement-a-lightweight-class-with-auto-implemented-properties

    [DataContract]
    public class CreateOrderCommand : IRequest<bool>
    {
        private readonly List<OrderItemDto> _orderItems;
        private List<BasketItem> items;
        private object city;
        private object street;
        private object state;
        private object country;
        private object zipCode;
        private object cardNumber;
        private object cardHolderName;
        private object cardExpiration;
        private object cardSecurityNumber;
        private object cardTypeId;

        public CreateOrderCommand(List<BasketItem> items, string userId, object city, object street, object state, object country, object zipCode, object cardNumber, object cardHolderName, object cardExpiration, object cardSecurityNumber, object cardTypeId)
        {
            this.items = items;
            UserId = userId;
            this.city = city;
            this.street = street;
            this.state = state;
            this.country = country;
            this.zipCode = zipCode;
            this.cardNumber = cardNumber;
            this.cardHolderName = cardHolderName;
            this.cardExpiration = cardExpiration;
            this.cardSecurityNumber = cardSecurityNumber;
            this.cardTypeId = cardTypeId;
        }

        [DataMember]
        public string UserId { get; private set; }
        [DataMember]
        public string Street { get; private set; }
        [DataMember]
        public string City { get; private set; }
        [DataMember]
        public string State { get; private set; }
        [DataMember]
        public string Country { get; private set; }
        [DataMember]
        public string ZipCode { get; private set; }
        [DataMember]
        public string UserName { get; private set; }
        [DataMember]
        public int CardTypeId { get; private set; }
        [DataMember]
        public string CardNumber { get; private set; }
        [DataMember]
        public string CardSecurityNumber { get; private set; }
        [DataMember]
        public string CardHolderName { get; private set; }
        [DataMember]
        public DateTime CardExpiration { get; private set; }
        [DataMember]
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