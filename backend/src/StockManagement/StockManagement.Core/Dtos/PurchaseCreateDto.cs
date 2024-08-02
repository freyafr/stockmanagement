namespace StockManagement.Core.Dtos
{
    public class PurchaseCreateDto
    {
        public Guid ClientId { get; init; }
        public Guid StockId { get; init; }
        public int Amount { get; init; }
    }
}
