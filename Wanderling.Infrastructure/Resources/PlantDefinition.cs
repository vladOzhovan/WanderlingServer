using Wanderling.Domain.Entities;

namespace Wanderling.Infrastructure.Resources
{
    public class PlantDefinition
    {
        public string SpeciesKey { get; set; } = string.Empty;
        public string DisplayedName { get; set; } = string.Empty;
        public string TypeName { get; set; } = string.Empty;
        public string Reproduction { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Rarity { get; set; } = string.Empty;
        public string IconPath { get; set; } = string.Empty;
        public List<PlantEffect> Effects { get; set; } = new();
        public List<string> Tags { get; set; } = new();
    }
}
