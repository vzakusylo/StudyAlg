using NUnit.Framework;
using OrderService.Domain;

namespace OrderService.Tests
{
    [TestFixture]
	public class ProductDiscountTests
	{
		private static readonly ProductBase MotorBasic = new BasicCarInsurance();
		private static readonly ProductBase MotorSuper = new SuperCarInsurance();
		private static readonly ProductBase Money = new DisabilityInsurance();

		[Test]
		public void Calculation_Single_MotorBasic_Gives_Discount()
		{
			Order order = new("Test Company", new DiscountRules(), new TaxCalculator());
			order.AddLine(new OrderLine(MotorBasic, 1));
			string actual = order.GenerateReceipt();

			string expected = $"Order receipt for 'Test Company'\r\n\t1 x Car Insurance Basic = kr 900,00\r\nSubtotal: kr 900,00\r\nMVA: kr 225,00\r\nTotal: kr 1 125,00";

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void Calculation_Many_MotorBasic_Gives_Discount()
		{
			Order order = new("Test Company", new DiscountRules(), new TaxCalculator());
			order.AddLine(new OrderLine(MotorBasic, 5));
			string actual = order.GenerateReceipt();

			string expected = $"Order receipt for 'Test Company'\r\n\t5 x Car Insurance Basic = kr 4 900,00\r\nSubtotal: kr 4 900,00\r\nMVA: kr 1 225,00\r\nTotal: kr 6 125,00";

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void Calculation_Single_MotorSuper_Gives_Discount()
		{
			Order order = new("Test Company", new DiscountRules(), new TaxCalculator());
			order.AddLine(new OrderLine(MotorSuper, 1));
			string actual = order.GenerateReceipt();

			string expected = $"Order receipt for 'Test Company'\r\n\t1 x Car Insurance Super = kr 1 900,00\r\nSubtotal: kr 1 900,00\r\nMVA: kr 475,00\r\nTotal: kr 2 375,00";

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void Calculation_Many_MotorSuper_Gives_Discount()
		{
			Order order = new("Test Company", new DiscountRules(), new TaxCalculator());
			order.AddLine(new OrderLine(MotorSuper, 3));
			string actual = order.GenerateReceipt();

			string expected = $"Order receipt for 'Test Company'\r\n\t3 x Car Insurance Super = kr 5 900,00\r\nSubtotal: kr 5 900,00\r\nMVA: kr 1 475,00\r\nTotal: kr 7 375,00";

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void Calculation_Many_Money_Gives_Discount()
		{
			Order order = new("Test Company", new DiscountRules(), new TaxCalculator());
			order.AddLine(new OrderLine(Money, 5));
			string actual = order.GenerateReceipt();

			string expected = $"Order receipt for 'Test Company'\r\n\t5 x Disability Insurance Basic = kr 4 900,00\r\nSubtotal: kr 4 900,00\r\nMVA: kr 1 225,00\r\nTotal: kr 6 125,00";

			Assert.AreEqual(expected, actual);
		}
	}
}