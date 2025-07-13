using System.Reflection;
using Wanderling.Domain.Attributes;
using Wanderling.Domain.Interfaces;
using Wanderling.Application.Interfaces;
using Wanderling.Domain.Entities.Collections.Plants;

namespace Wanderling.Infrastructure.Factories
{
    public class PlantFactory : IOrganismFactory
    {
        private readonly IDictionary<string, Type> _plantRegistry;

        public PlantFactory()
        {
            _plantRegistry = Assembly.GetAssembly(typeof(Plant))!
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Type)) && !t.IsAbstract)
                .Select(t => new
                {
                    Type = t,
                    Attr = t.GetCustomAttribute<PlantMetadataAttribute>()
                })
                .Where(x => x.Attr != null)
                .ToDictionary(
                    x => x.Attr.TypeKey.ToLower(),
                    x => x.Type
                );
        }

        public IOrganism Create(string name, string typeName, IReproduction reproduction)
        {
            if (!_plantRegistry.TryGetValue(typeName.ToLower(), out var type))
                throw new ArgumentException($"Unknown plant type: {typeName}");

            try
            {
                var plant = (IOrganism)Activator.CreateInstance(type, name, reproduction);
                return plant;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to create plant of type '{type.Name}'", ex);
            }
        }
    }
}
