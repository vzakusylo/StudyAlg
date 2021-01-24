namespace OrderService.Domain
{
    public abstract class CarInsuranceBase : ProductBase
    {
        public override ProductType ProductType => new CarInsuranceType();
    }
}