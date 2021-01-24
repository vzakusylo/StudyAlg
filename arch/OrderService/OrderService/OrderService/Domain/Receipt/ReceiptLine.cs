namespace OrderService.Domain
{
    public class ReceiptLine
    {
		public ReceiptLine(int quantity, ProductType productType, string productName, double amount)
        {
			ProductType = productType;
			Quantity = quantity;
			ProductName = productName;
			Amount = amount;
        }
		public ProductType ProductType { get; }
		public int Quantity { get; }
		public string ProductName { get; set; }
		public double Amount { get; set; }
	}
}