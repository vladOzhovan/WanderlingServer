using Wanderling.Domain.Entities;

namespace Wanderling.Domain.Interfaces
{
    public interface IOrganismFactory
    {
        Organism Create(string typeKey, string name);
    }
}
