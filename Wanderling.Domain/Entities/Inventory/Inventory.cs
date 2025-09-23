namespace Wanderling.Domain.Entities.Inventory
{
    public class Inventory
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<InventoryItem> Items { get; set; } = new();
        public float MaxWeight { get; set; } = 100;
        public float CurrentWeight => Items.Sum(i => i.TotalWeight);
        public bool Overencumbered => CurrentWeight > MaxWeight;
    }
}
