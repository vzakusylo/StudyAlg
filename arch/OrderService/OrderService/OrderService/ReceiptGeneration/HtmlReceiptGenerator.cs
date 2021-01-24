using OrderService.Domain;
using System.Linq;
using System.Text;

namespace OrderService.ReceiptGenerator
{
    public class HtmlReceiptGenerator : ReceiptGeneratorBase
    {
        public HtmlReceiptGenerator(Order order, string cultureInfo = "nb-NO") : 
            base(order, cultureInfo)
        {
        }

        protected override string GenerateReceipt(Receipt receipt)
        {
            StringBuilder result = new($"<html><body><h1>Order receipt for '{receipt.CompanyName}'</h1>");
            
            if (receipt.Lines.Any())
            {
                result.Append("<ul>");
                foreach (var line in receipt.Lines)
                {
                    result.Append($"<li>{line.Quantity} x {line.ProductType.DisplayName} {(string.IsNullOrEmpty(line.ProductName) ? "" : line.ProductName + " ")}= {line.Amount:C}</li>");
                }
                result.Append("</ul>");
            }
            result.Append($"<h3>Subtotal: {receipt.Subtotal:C}</h3>");
            result.Append($"<h3>MVA: {receipt.TotalTax:C}</h3>");
            result.Append($"<h2>Total: {receipt.TotalAmount:C}</h2>");
            result.Append("</body></html>");
            return result.ToString();
        }
    }
}
