using Wanderling.Domain.Attributes;
using Wanderling.Domain.Interfaces;

namespace Wanderling.Domain.Entities.Collections.Plants
{
    [PlantMetadata("shrub")]
    public class Shrub : Plant
    {
        public Shrub(string speciesName, IReproduction reproduction) : base(speciesName, reproduction)
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
