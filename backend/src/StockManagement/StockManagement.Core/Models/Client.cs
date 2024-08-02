namespace StockManagement.Core.Models
{
    public class Client
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public required string Name { get; init; }
        public required string LastName { get; init; }
        public required string Email { get; init; }
        public decimal Balance { get; set; }
    }
}
