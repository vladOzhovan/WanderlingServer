using Wanderling.Domain.Entities;

namespace Wanderling.Infrastructure.Entities
{
    public class UserPlantEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CatalogPlantId { get; set; }
        public string SpeciesName { get; set; } = string.Empty;
        public string DisplayedName { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Reproduction { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Rarity { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int Count { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<PlantEffect> Effects { get; set; } = new();
    }
}
