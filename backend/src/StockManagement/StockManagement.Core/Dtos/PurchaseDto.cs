namespace StockManagement.Core.Dtos
{
    public class PurchaseDto
    {
        public Guid Id { get; init; }
        public Guid ClientId { get; init; }
        public required string StockName { get; init; }
        public decimal Price { get; init; }
        public int Amount { get; init; }
        public decimal TotalPrice { get; init; }
        public DateTime PurchaseDate { get; init; }
    }
}
