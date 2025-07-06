using Wanderling.Domain.Attributes;
using Wanderling.Domain.Interfaces;

namespace Wanderling.Domain.Entities.Collections.Plants
{
    [OrganismMetadata("plant")]
    public abstract class Plant : Organism
    {
        protected Plant(IReproductionStrategy strategy) : base(strategy)
        {
            CreatedAt = DateTime.UtcNow;
        }
        
        //public Dictionary<string, object> Properties { get; } = new();
    }
}
