using Wanderling.Domain.Entities;

namespace Wanderling.Application.Models
{
    public class PlantModel
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; } = Guid.Empty;
        public string SpeciesName { get; set; } = string.Empty;
        public string DisplayedName { get; set; } = string.Empty;
        public string TypeName { get; set; } = string.Empty;
        public string Reproduction { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Rarity { get; set; } = string.Empty;
        public List<PlantEffect> Effects { get; set; } = new();
        public DateTime CreatedAt { get; set; }
    }
}
