namespace OrderService.Domain
{
    public abstract class ProductBase
	{
		public abstract ProductType ProductType { get; }
		public abstract string ProductName { get; }
		public abstract int Price { get; }
    }
}