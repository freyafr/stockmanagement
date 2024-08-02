namespace StockManagement.Core.Models
{
    public class Stock
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public required string Name { get; init; }
        public decimal Price { get; init; }
    }
}
