namespace Wanderling.Application.Models
{
    public class PlantModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Reproduction { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
