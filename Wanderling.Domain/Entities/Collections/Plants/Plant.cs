using Wanderling.Domain.Interfaces;

namespace Wanderling.Domain.Entities.Collections.Plants
{
    public abstract class Plant : Organism
    {
        protected Plant(string name, IReproduction reproduction) : base(name, reproduction)
        {
        }
    }
}
