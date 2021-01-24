namespace OrderService.Domain
{
    public class DisabilityInsurance : ProductBase
    {
        public override ProductType ProductType => new DisabilityInsuranceType();

        public override string ProductName => "Basic";

        public override int Price => Prices.OneThousand;
    }
}