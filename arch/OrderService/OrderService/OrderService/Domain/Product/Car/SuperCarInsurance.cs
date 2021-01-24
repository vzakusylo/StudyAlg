namespace OrderService.Domain
{
    public class SuperCarInsurance : CarInsuranceBase
    {
        public override string ProductName => "Super";

        public override int Price => Prices.TwoThousand;
    }
}