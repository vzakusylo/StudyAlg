namespace OrderService.Domain
{
    public interface IDiscount
    {
        public double Amount { get; }
        public bool CanApply();

        public byte Priority { get; }
    }
}