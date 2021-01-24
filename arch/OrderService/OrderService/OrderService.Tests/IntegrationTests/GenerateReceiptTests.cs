using System.Globalization;
using NUnit.Framework;
using OrderService.Domain;

namespace OrderService.Tests
{
    [TestFixture]
	public class GenerateReceiptTests
	{
		private static readonly ProductBase MotorBasic = new BasicCarInsurance();	

		[Test]
		public void Can_generate_html_receipt()
		{
			Order order = new("Test Company", new DiscountRules(), new TaxCalculator());
			order.AddLine(new OrderLine(MotorBasic, 1));

			string actual = order.GenerateHtmlReceipt();

			string expected = $"<html><body><h1>Order receipt for 'Test Company'</h1><ul><li>1 x Car Insurance Basic = kr 900,00</li></ul><h3>Subtotal: kr 900,00</h3><h3>MVA: kr 225,00</h3><h2>Total: kr 1 125,00</h2></body></html>";

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void Can_generate_text_receipt()
		{
			Order order = new("Test Company", new DiscountRules(), new TaxCalculator());
			order.AddLine(new OrderLine(MotorBasic, 1));
			string actual = order.GenerateReceipt();

			string expected = $"Order receipt for 'Test Company'\r\n\t1 x Car Insurance Basic = kr 900,00\r\nSubtotal: kr 900,00\r\nMVA: kr 225,00\r\nTotal: kr 1 125,00";

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void Can_generate_json_receipt()
		{
			Order order = new("Test Company", new DiscountRules(), new TaxCalculator());
			order.AddLine(new OrderLine(MotorBasic, 1));
			string actual = order.GenerateJsonReceipt();

			string expected = "{\"CompanyName\":\"Test Company\",\"TotalAmount\":1125,\"TotalTax\":225,\"Subtotal\":900,\"Lines\":[{\"ProductType\":{\"Type\":\"CarInsurance\",\"DisplayName\":\"Car Insurance\"},\"Quantity\":1,\"ProductName\":\"Basic\",\"Amount\":900}]}";

			Assert.AreEqual(expected, actual);
		}
	}
}