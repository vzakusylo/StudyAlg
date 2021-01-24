using OrderService.Domain;
using Xunit;
using Moq;
using RandomTestValues;

namespace OrderService.Tests
{
    public class OrderLineTests
    {
        [Fact]
        public void DiscountCalculated_WhenOrderLineCalculateCalled()
        {
            var price = Prices.OneThousand;
            var quantity = RandomValue.Int();
            ProductBase product = new BasicCarInsurance();
            OrderLine orderLine = new OrderLine(product, quantity);
              var discountRulesMock = new Mock<IDiscountRules>();
              discountRulesMock.Setup(x => x.GetDiscount(It.IsAny<OrderLine>())).Returns(new NoDiscount(0));

            orderLine.Calculate(discountRulesMock.Object);

            var expectedAmount = price * quantity;
            Assert.Equal(expectedAmount, orderLine.TotalAmount);

            discountRulesMock.Verify(x => x.GetDiscount(It.IsAny<OrderLine>()), Times.Once());
        }
    }
}