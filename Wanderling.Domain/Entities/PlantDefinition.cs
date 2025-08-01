namespace Wanderling.Domain.Entities
{
    public class PlantDefinition
    {
        public string ScientificName { get; set; } = string.Empty;
        public string DisplayedName { get; set; } = string.Empty;
        public string TypeName { get; set; } = string.Empty;
        public string Reproduction { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Rarity { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public List<PlantEffect> Effects { get; set; } = new();
        public List<string> Tags { get; set; } = new();
    }
}
