using Wanderling.Domain.Attributes;
using Wanderling.Domain.Interfaces;
using Wanderling.Domain.Strategies;

namespace Wanderling.Domain.Entities.Collections.Plants
{
    [OrganismMetadata("herb", typeof(SeedReproduction)]
    public class Herb : Plant
    {
        public Herb(IReproductionStrategy strategy) : base(strategy)
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
