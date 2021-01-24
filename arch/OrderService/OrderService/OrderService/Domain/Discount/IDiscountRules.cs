namespace OrderService.Domain
{
    public interface IDiscountRules
    {
        IDiscount GetDiscount(OrderLine orderLine);
    }
}
