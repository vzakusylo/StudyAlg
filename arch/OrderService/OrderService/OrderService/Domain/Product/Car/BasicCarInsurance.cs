namespace OrderService.Domain
{
    public class BasicCarInsurance : CarInsuranceBase
    {
        public override string ProductName => "Basic";

        public override int Price => Prices.OneThousand;
    }
}