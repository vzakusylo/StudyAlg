using OrderService.Domain;

namespace OrderService.ReceiptGenerator
{
    public abstract class ReceiptGeneratorBase
    {
        public ReceiptGeneratorBase(Order order, string cultureInfo)
        {
            Order = order;
            System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo(cultureInfo);
        }
       
        public Order Order { get; }       

        protected abstract string GenerateReceipt(Receipt receipt);

        public string GetReceipt()
        {
            Receipt receipt = Receipt.CreateReceipt(Order);
            return GenerateReceipt(receipt);
        }
    }
}
