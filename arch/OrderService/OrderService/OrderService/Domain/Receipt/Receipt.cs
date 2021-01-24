using System.Collections.Generic;

namespace OrderService.Domain
{
    public class Receipt
    {
		private Receipt(string companyName, 
			double subtotal, 
			double totalAmount, 
			double tax, 
			List<ReceiptLine> receiptLines)
        {
			Subtotal = subtotal;
            TotalAmount = totalAmount;
			TotalTax = tax;
			Lines = receiptLines;
			CompanyName = companyName;
        }

		public string CompanyName { get; }
		public double TotalAmount { get; }
		public double TotalTax { get; }
		public double Subtotal { get; }
		public IReadOnlyList<ReceiptLine> Lines { get; }

		public static Receipt CreateReceipt(Order order)
        {		
            var receiptLines = new List<ReceiptLine>();
			foreach (OrderLine line in order.OrderLines)
			{				
				receiptLines.Add(new ReceiptLine(line.Quantity, line.Product.ProductType, line.Product.ProductName, line.TotalAmount));					
			}
			return new Receipt(order.CompanyName, order.Subtotal, order.TotalAmount, order.TotalTax, receiptLines);
		}
    }
}