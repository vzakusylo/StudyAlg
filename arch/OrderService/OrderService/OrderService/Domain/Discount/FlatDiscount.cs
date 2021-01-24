namespace OrderService.Domain
{
    public class FlatDiscount : IDiscount
    {
        public FlatDiscount(double discountAmount, byte priority)
        {
            Amount = discountAmount;
            Priority = priority;
        }

        public double Amount { get; }

        public byte Priority { get; }

        public bool CanApply() => true;
    }
}