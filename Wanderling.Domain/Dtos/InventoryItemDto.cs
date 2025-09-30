namespace Wanderling.Domain.Dtos
{
    public sealed class InventoryItemDto
    {
        public Guid ItemInstanceId { get; init; }
        public Guid DefinitionId { get; init; }
        public string Name { get; init; } = string.Empty;
        public int Quantity { get; init; }
        public float TotalWeight { get; init; }
    }
}
