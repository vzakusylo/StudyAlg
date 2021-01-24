namespace OrderService.Domain
{
    public abstract class ProductType
    {
		public abstract string Type { get; }

		public abstract string DisplayName { get; }
    }
}