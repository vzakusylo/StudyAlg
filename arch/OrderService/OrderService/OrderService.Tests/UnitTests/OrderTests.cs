using Moq;
using OrderService.Domain;
using RandomTestValues;
using Xunit;

namespace OrderService.Tests.UnitTests
{
    public class OrderTests
    {
        [Fact]
        public void WhenOrderAddLineCalled_OrderIsRecalculated()
        {
            var price = Prices.OneThousand;
            var quantity = RandomValue.Int(1000, 0);
            var totalTax = RandomValue.Int(10,0);
       
            var discountRulesMock = new Mock<IDiscountRules>();
            discountRulesMock.Setup(x => x.GetDiscount(It.IsAny<OrderLine>())).Returns(new NoDiscount(0));
            var taxCalculator = new Mock<ITaxCalculator>();
            taxCalculator.Setup(x => x.GetTotalTax(It.IsAny<double>())).Returns(totalTax);
        
            Order order = new Order("Company name", discountRulesMock.Object, taxCalculator.Object);
            order.AddLine(new OrderLine(new BasicCarInsurance(), quantity));

            var expectedSubTotal = quantity * price;
            Assert.Equal(expectedSubTotal, order.Subtotal);
            Assert.Equal(totalTax, order.TotalTax);
            Assert.Equal(totalTax + expectedSubTotal, order.TotalAmount);

            discountRulesMock.Verify(x => x.GetDiscount(It.IsAny<OrderLine>()), Times.Once());
            taxCalculator.Verify(x => x.GetTotalTax(price * quantity), Times.Once());
        }
    }
}
