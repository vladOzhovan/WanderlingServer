namespace Wanderling.Application.Models
{
    public class PlantIdentifiedModel
    {
        public Guid Id { get; set; }
        public string SpeciesName { get; set; } = string.Empty;
        public string ScientificName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime? DiscoverededAt { get; set; }
    }
}
