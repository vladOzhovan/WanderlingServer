using Wanderling.Domain.Enums;

namespace Wanderling.Domain.Entities.Inventory
{
    public class ItemDefinition
    {
        public Guid Id { get; set; }
        public string Key { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public ItemType ItemType { get; set; }
        public bool Droppable { get; set; } = true;
        public bool Tradeable { get; set; } = true;
        public float Weight { get; set; }
        public string[] Tags { get; set; }
    }
}
