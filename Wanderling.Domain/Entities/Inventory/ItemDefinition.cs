using Wanderling.Domain.Enums;

namespace Wanderling.Domain.Entities.Inventory
{
    public class ItemDefinition
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ItemType ItemType { get; set; }
        public float Weight { get; set; }
        public string[] Tags { get; set; }
    }
}
