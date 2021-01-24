using OrderService.Domain;
using RandomTestValues;
using Xunit;

namespace OrderService.Tests
{
    public class DiscountTests
    {
        [Fact]
        public void WhenNoDiscountApplied_AmountHasNotChanged()
        {
            var noDiscount = new NoDiscount(0);

            Assert.Equal(0, noDiscount.Amount);
            Assert.Equal(0, noDiscount.Priority);
            Assert.True(noDiscount.CanApply());
        }

        [Theory]
        [InlineData(100, true)]
        [InlineData(200, true)]
        public void WhenFlatDiscountApplied_AmountHasChanged(int expectedAmount, bool shouldBeApplied)
        {
            var flatDiscount = new FlatDiscount(expectedAmount, 1);

            Assert.Equal(expectedAmount, flatDiscount.Amount);
            Assert.Equal(1, flatDiscount.Priority);
            Assert.Equal(shouldBeApplied, flatDiscount.CanApply());
        }

        [Theory]
        [InlineData(.10d, 1, 2, Prices.OneThousand, 0, false)]
        [InlineData(.10d, 2, 2, Prices.OneThousand, 200, true)]

        public void WhenProductPriceQuantityPercentDiscountApplied_AmountHasChanged(double percent, int orderQuantity, int discountQuantity, int price, int expectedAmount, bool shouldBeApplied)
        {
            var orderLine = new OrderLine(new BasicCarInsurance(), orderQuantity);
            var discount = new ProductPriceQuantityPercentDiscount(orderLine, percent, discountQuantity, price, 0);

            Assert.Equal(expectedAmount, discount.Amount);
            Assert.Equal(0, discount.Priority);
            Assert.Equal(shouldBeApplied, discount.CanApply());
        }

        [Theory]
        [InlineData(.10d, 1, 2, 0, false)]
        [InlineData(.10d, 2, 2, 200, true)]

        public void WhenTwoThousandPriceWithMoreTreeProductsDiscountApplied_AmountHasChanged(double percent, int orderQuantity, int discountQuantity, int expectedAmount, bool shouldBeApplied)
        {
            var orderLine = new OrderLine(new BasicCarInsurance(), orderQuantity);
            var discount = new ProductQuantityPercentDiscount(orderLine, percent, discountQuantity, 0);

            Assert.Equal(expectedAmount, discount.Amount);
            Assert.Equal(0, discount.Priority);
            Assert.Equal(shouldBeApplied, discount.CanApply());
        }
    }
}
