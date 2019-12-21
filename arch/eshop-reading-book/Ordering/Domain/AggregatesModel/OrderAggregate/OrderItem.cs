using Ordering.Domain.Exceptions;
using Ordering.Domain.SeedWork;

namespace Ordering.Domain.AggregatesModel.OrderAggregate
{
    public class OrderItem : Entity
    {
        private string productName;
        private decimal unitPrice;
        private decimal discount;
        private string pictureUrl;
        private int units;

        public OrderItem(int productId, string productName, decimal unitPrice, decimal discount, string pictureUrl, int units)
        {
            if (units <= 0)
            {
                throw new OrderingDomainException("Invalid number of units");
            }
            if ((unitPrice * units) < discount)
            {
                throw new OrderingDomainException("The total of order item is lower than applied discount");
            }
            ProductId = productId;

            this.productName = productName;
            this.unitPrice = unitPrice;
            this.discount = discount;
            this.pictureUrl = pictureUrl;
            this.units = units;
        }

        public int ProductId { get; private set; }

        public decimal GetCurrentDiscount()
        {
            return this.discount;
        }

        public void SetNewDiscount(decimal discount)
        {
            if (discount < 0)
            {
                throw new OrderingDomainException("Discount is not valid");
            }
            this.discount = discount;
        }

        public void AddUnits(int units)
        {
            if (units < 0)
            {
                throw new OrderingDomainException("Invalid units");
            }

            this.units += units;
        }

        public int GetUnits()
        {
            return this.units;
        }

        public decimal GetUnitPrice()
        {
            return this.unitPrice;
        }
    }
}
