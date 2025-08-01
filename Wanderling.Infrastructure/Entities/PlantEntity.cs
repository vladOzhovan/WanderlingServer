using Wanderling.Domain.Entities;

namespace Wanderling.Infrastructure.Entities
{
    public class PlantEntity
    {
        public Guid Id { get; set; }
        public string ScientificName { get; set; } = string.Empty;
        public string DisplayedName { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Reproduction { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Rarity { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<PlantEffect> Effects { get; set; } = new();
    }
}
