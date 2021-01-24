namespace OrderService.Domain
{
    public class ProductQuantityPercentDiscount : IDiscount
    {
        private readonly OrderLine orderLine;
        private readonly int quantity;

        public ProductQuantityPercentDiscount(OrderLine orderLine, double percent, int quantity, byte priority)
        {
            this.orderLine = orderLine;
            this.quantity = quantity;
            this.Priority = priority;

            if (CanApply())
                Amount = orderLine.Quantity * orderLine.Product.Price * percent;
        }

        public double Amount { get; }

        public byte Priority { get; }

        public bool CanApply() => orderLine.Quantity >= quantity;
       
    }
}