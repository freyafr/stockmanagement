namespace StockManagement.Core.Dtos
{
    public class PurchaseCreateResponseDto
    {
        public Guid ClientId { get; init; }
        public Guid PurchaseId { get; init; }
        public DateTime PurchaseDate { get; init; }
        public decimal TotalPrice { get; init; }
    }
}
