﻿namespace StockManagement.Core.Dtos
{
    public class StockDto
    {
        public Guid Id { get; init; }
        public required string Name { get; init; }
        public decimal Price { get; init; }
    }
}
