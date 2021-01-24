using OrderService.ReceiptGenerator;
using System.Collections.Generic;
using System.Linq;

namespace OrderService.Domain
{
    public class Order
	{
		private readonly List<OrderLine> _orderLines = new List<OrderLine>();
        private readonly IDiscountRules discountRules;
        private readonly ITaxCalculator taxCalculator;

        public Order(string companyName, IDiscountRules discountRules, ITaxCalculator taxCalculator)
		{
			CompanyName = companyName;
            this.discountRules = discountRules;
            this.taxCalculator = taxCalculator;
        }

		public string CompanyName { get; }       
        public double TotalAmount { get; private set; }
		public double TotalTax { get; private set; }
		public double Subtotal { get; private set; }
		public IReadOnlyList<OrderLine> OrderLines => _orderLines.AsReadOnly();

		public void AddLine(OrderLine orderLine)
		{
			orderLine.Calculate(discountRules);
			_orderLines.Add(orderLine);

			Subtotal = _orderLines.Sum(x=>x.TotalAmount);
			TotalTax = taxCalculator.GetTotalTax(Subtotal);
			TotalAmount = Subtotal + TotalTax;
		}

		public string GenerateReceipt()
		{
			ReceiptGeneratorBase generator = new TextReceiptGenerator(this);
			return generator.GetReceipt();
		}

		public string GenerateHtmlReceipt()
		{
			ReceiptGeneratorBase htmlGenerator = new HtmlReceiptGenerator(this);
			return htmlGenerator.GetReceipt();
		}

		public string GenerateJsonReceipt()
		{
			ReceiptGeneratorBase htmlGenerator = new JsonReceiptGenerator(this);
			return htmlGenerator.GetReceipt();
		}
	}
}