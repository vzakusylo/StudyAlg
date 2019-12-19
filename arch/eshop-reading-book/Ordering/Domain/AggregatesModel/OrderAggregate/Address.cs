using Ordering.Domain.SeedWork;

namespace Ordering.Domain.AggregatesModel.OrderAggregate
{
    public class Address : ValueObject
    {
        public string Street { get; private set; }

        public Address() { }

        public Address(string street)
        {
            Street = street;
        }
    }
}
