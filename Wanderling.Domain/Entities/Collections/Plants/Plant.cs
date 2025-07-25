using Wanderling.Domain.Interfaces;

namespace Wanderling.Domain.Entities.Collections.Plants
{
    public abstract class Plant : Organism
    {
        protected Plant(string speciesName, IReproduction reproduction) : base(speciesName, reproduction) { }

        public string Rarity { get; set; } = null!;
        public List<PlantEffect> Effects { get; set; } = new();
    }
}
