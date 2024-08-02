namespace StockManagement.Core.Models
{
    public class Purchase
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public Guid ClientId { get; init; }
        public Guid StockId { get; init; }
        public int Amount { get; init; }
        public DateTime PurchaseDate { get; init; } = DateTime.UtcNow; // to do: needs to be initiated in DbContext during Saving

        public virtual Client? Client { get; set; } = null!;
        public virtual Stock? Stock { get; set; } = null!;
    }
}
