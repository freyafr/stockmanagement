namespace StockManagement.Core.Dtos
{
    public class ClientDto
    {
        public Guid Id { get; init; }
        public required string Name { get; init; }
        public required string LastName { get; init; }
        public required string Email { get; init; }
        public decimal Balance { get; init; }
    }
}
