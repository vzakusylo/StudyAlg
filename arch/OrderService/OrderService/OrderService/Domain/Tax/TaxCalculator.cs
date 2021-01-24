namespace OrderService.Domain
{
    public class TaxCalculator : ITaxCalculator
    {
        private const double TaxRate = .25d;

        public double GetTotalTax(double subtotal) => subtotal * TaxRate;
    }
}
