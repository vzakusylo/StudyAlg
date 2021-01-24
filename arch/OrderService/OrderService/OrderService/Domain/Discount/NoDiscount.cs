namespace OrderService.Domain
{

    public class NoDiscount : IDiscount
    {
        public NoDiscount(byte priority)
        {
            Priority = priority;
        }

        public double Amount => 0;

        public byte Priority { get; }

        public bool CanApply() => true;
    }
}