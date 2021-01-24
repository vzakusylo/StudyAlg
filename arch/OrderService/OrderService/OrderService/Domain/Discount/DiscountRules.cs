using System.Linq;

namespace OrderService.Domain
{
    public class DiscountRules : IDiscountRules
    {
        public static IDiscount[] GetRules(OrderLine orderLine) {           
            return new IDiscount[] {
                new ProductPriceQuantityPercentDiscount(orderLine, .1d, Prices.OneThousand, 5, 4),
                new ProductPriceQuantityPercentDiscount(orderLine, .2d, Prices.TwoThousand, 3, 3),
                new ProductQuantityPercentDiscount(orderLine, .5d, 10, 2),
                new FlatDiscount(100, 1),
                new NoDiscount(0)
            };
        }

        public IDiscount GetDiscount(OrderLine orderLine)
        {
            return GetRules(orderLine).OrderByDescending(x=>x.Priority).FirstOrDefault(x => x.CanApply()) ?? new NoDiscount(0);
        }
    }
}
