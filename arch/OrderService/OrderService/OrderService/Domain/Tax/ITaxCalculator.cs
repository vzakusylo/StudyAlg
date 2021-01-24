namespace OrderService.Domain
{
    public interface ITaxCalculator
    {
        double GetTotalTax(double subtotal);
    }
}
