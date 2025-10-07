using Wanderling.Domain.Enums;

namespace Wanderling.Domain.Entities.Inventory
{
    public class ItemDefinition
    {
        public Guid Id { get; set; }
        public string Key { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string[]? Tags { get; set; }
        public decimal Weight { get; set; }
        public bool IsDroppable { get; set; } = true;
        public bool IsTradeable { get; set; } = true;
        public bool IsFavorites { get; set; } = false;
        public bool IsForQuest { get; set; } = false;
        public ItemType ItemType { get; set; }
    }
}
