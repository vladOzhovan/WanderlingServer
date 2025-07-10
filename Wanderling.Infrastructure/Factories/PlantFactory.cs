using System.Reflection;
using Wanderling.Application.Interfaces;
using Wanderling.Domain.Attributes;
using Wanderling.Domain.Entities.Collections.Plants;
using Wanderling.Domain.Interfaces;

namespace Wanderling.Infrastructure.Factories
{
    public class PlantFactory : IOrganismFactory
    {
        private readonly IDictionary<string, Type> _plantRegistry;

        public PlantFactory()
        {
            _plantRegistry = Assembly.GetAssembly(typeof(Plant))!
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Plant)) && !t.IsAbstract)
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
            var key = typeName.ToLower();

            if (!_plantRegistry.TryGetValue(key, out var type))
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
