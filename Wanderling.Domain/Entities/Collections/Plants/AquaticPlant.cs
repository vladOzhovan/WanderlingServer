using Wanderling.Domain.Attributes;
using Wanderling.Domain.Interfaces;

namespace Wanderling.Domain.Entities.Collections.Plants
{
    [OrganismMetadata("AquaticPlant")]
    public class AquaticPlant : Plant
    {
        public AquaticPlant(IReproductionStrategy strategy) : base(strategy)
        {
        }

        public override void Born()
        {
            throw new NotImplementedException();
        }

        public override void Die()
        {
            throw new NotImplementedException();
        }

        public override void Grow()
        {
            throw new NotImplementedException();
        }
    }
}
