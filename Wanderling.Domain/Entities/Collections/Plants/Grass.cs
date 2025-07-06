using Wanderling.Domain.Attributes;
using Wanderling.Domain.Interfaces;

namespace Wanderling.Domain.Entities.Collections.Plants
{
    [OrganismMetadata("grass")]
    public class Grass : Plant
    {
        public Grass(IReproductionStrategy strategy) : base(strategy)
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
