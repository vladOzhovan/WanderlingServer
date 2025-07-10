using Wanderling.Domain.Attributes;
using Wanderling.Domain.Interfaces;

namespace Wanderling.Domain.Entities.Collections.Plants
{
    [PlantMetadata("aquaticPlant")]
    public class AquaticPlant : Plant
    {
        public AquaticPlant(string name, IReproduction reproduction) : base(name, reproduction)
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
