using Wanderling.Domain.Interfaces;

namespace Wanderling.Application.Interfaces
{
    public interface IOrganismFactory
    {
        IOrganism Create(string name, string typeName, IReproduction reproduction);
    }
}
