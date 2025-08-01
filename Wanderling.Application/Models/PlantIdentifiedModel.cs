namespace Wanderling.Application.Models
{
    public class PlantIdentifiedModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string ScientificName { get; set; } = string.Empty;
        public string DisplayedName { get; set; } = string.Empty;
        public string TypeName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Reproduction { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime DiscoverededAt { get; set; }
    }
}
