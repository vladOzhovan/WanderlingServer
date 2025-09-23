namespace Wanderling.Domain.Entities.Inventory
{
    public class InventoryItem
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ItemDefinition Item { get; set; } = new();
        public int Quantity { get; set; }
        public float TotalWeight => Item.Weight * Quantity;
    }
}
