namespace Wanderling.Domain.Entities.Inventory
{
    public class InventoryItem
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalWeight => Item.Weight * Quantity;
        public ItemDefinition Item { get; set; } = new();
    }
}
