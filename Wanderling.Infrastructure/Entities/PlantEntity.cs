using Wanderling.Domain.Entities;

namespace Wanderling.Infrastructure.Entities
{
    public class PlantEntity
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; } = Guid.Empty;
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Reproduction { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public List<PlantEffect> Effects { get; set; } = new();
    }
}
