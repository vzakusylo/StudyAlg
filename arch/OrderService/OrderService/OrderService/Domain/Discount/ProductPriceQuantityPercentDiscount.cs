namespace OrderService.Domain
{
    public class ProductPriceQuantityPercentDiscount : IDiscount
    {
        private readonly OrderLine orderLine;
        private readonly int quantity;
        private readonly double price;

        public ProductPriceQuantityPercentDiscount(OrderLine orderLine, double percent, int quantity, double price, byte priority)
        {
            this.orderLine = orderLine;
            this.quantity = quantity;
            this.price = price;

            if (CanApply())
                Amount = orderLine.Quantity * orderLine.Product.Price * percent;
        }

        public double Amount { get; }

        public byte Priority { get; }

        public bool CanApply() => orderLine.Quantity >= quantity && orderLine.Product.Price == price;

    }
}