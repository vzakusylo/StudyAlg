using OrderService.Domain;
using System;
using System.Text;

namespace OrderService.ReceiptGenerator
{
    public class TextReceiptGenerator : ReceiptGeneratorBase
    {
        public TextReceiptGenerator(Order order, string cultureInfo= "nb-NO") : base(order, cultureInfo)
        {
        }

        protected override string GenerateReceipt(Receipt receipt)
        {
            StringBuilder result = new($"Order receipt for '{receipt.CompanyName}'{Environment.NewLine}");
            foreach (var line in receipt.Lines)
            {
                result.AppendLine($"\t{line.Quantity} x {line.ProductType.DisplayName} {line.ProductName} = {  line.Amount:C}");
            }
            result.AppendLine($"Subtotal: {receipt.Subtotal:C}");
            result.AppendLine($"MVA: {receipt.TotalTax:C}");
            result.Append($"Total: {receipt.TotalAmount:C}");

            return result.ToString();
        }
    }
}
