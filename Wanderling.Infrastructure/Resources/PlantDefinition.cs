namespace Wanderling.Infrastructure.Resources
{
    public class PlantDefinition
    {
        public string TypeName { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Rarity { get; set; } = null!;
        public string IconPath { get; set; } = null!;
        public List<string> Tags { get; set; } = new();
    }
}
