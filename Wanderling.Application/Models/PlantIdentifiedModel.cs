namespace Wanderling.Application.Models
{
    /// <summary>
    /// Model of a Plant identified from a photograph
    /// </summary>
    public class PlantIdentifiedModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string ScientificName { get; set; } = string.Empty;
        public string DisplayedName { get; set; } = string.Empty;
        public string TypeName { get; set; } = string.Empty;
        public string Reproduction { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Rarity { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime DiscoverededAt { get; set; }
    }
}
