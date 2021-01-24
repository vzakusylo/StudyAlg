namespace OrderService.Domain
{
	public class OrderLine
	{
        private readonly IDiscountRules discountRules;

        public OrderLine(ProductBase product, int quantity)
		{
			Product = product;
			Quantity = quantity;
        }

		public ProductBase Product { get; }
		public int Quantity { get; }
		public double TotalAmount { get; private set; }
		public double DiscountAmount { get; protected set; }

		public void Calculate(IDiscountRules discountRules)
		{
			var discount = discountRules.GetDiscount(this);
			DiscountAmount = discount.Amount;
			TotalAmount = Product.Price * Quantity - DiscountAmount;
		}
	}
}