using OrderService.Domain;
using System.Text.Json;

namespace OrderService.ReceiptGenerator
{
    public class JsonReceiptGenerator : ReceiptGeneratorBase
    {
        public JsonReceiptGenerator(Order order, string cultureInfo= "nb-NO") : 
            base(order, cultureInfo)
        {
        }

        protected override string GenerateReceipt(Receipt receipt)
        {
            return JsonSerializer.Serialize(receipt);
        }
    }
}
