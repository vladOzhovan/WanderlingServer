using System.Reflection;
using System.Text.Json;
using Wanderling.Application.Interfaces;
using Wanderling.Domain.Attributes;
using Wanderling.Domain.Entities.Collections.Plants;
using Wanderling.Domain.Interfaces;
using Wanderling.Infrastructure.Resources;

namespace Wanderling.Infrastructure.Factories
{
    public class PlantFactory : IOrganismFactory
    {
        private readonly IDictionary<string, Type> _plantTypesMap;
        private readonly IList<PlantDefinition> _definitions;

        public PlantFactory(string plantRegPath)
        {
            var json = File.ReadAllText(plantRegPath);

            _definitions = JsonSerializer.Deserialize<List<PlantDefinition>>(json)
                           ?? new List<PlantDefinition>();

            var baseType = typeof(Plant);

            _plantTypesMap = Assembly.GetAssembly(typeof(Plant))!
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

        public IOrganism Create(string speciesName, string typeName, IReproduction reproduction)
        {
            if (!_plantTypesMap.TryGetValue(typeName.ToLower(), out var plantType))
                throw new ArgumentException($"Unknown plant type: {typeName}");

            var plantDefinition = _definitions.FirstOrDefault(d =>
                    string.Equals(d.SpeciesKey, speciesName, StringComparison.OrdinalIgnoreCase));

            if (plantDefinition == null)
                throw new InvalidOperationException($"Missing plant definition for {speciesName}");

            try
            {
                var plant = (Plant)Activator.CreateInstance(plantType, speciesName, reproduction);

                if (plant != null)
                {
                    plant.DisplayedName = plantDefinition.DisplayedName;
                    plant.Description = plantDefinition.Description;
                    plant.Rarity = plantDefinition.Rarity;
                    plant.Effects = plantDefinition.Effects;
                }

                return plant;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to create plant of type '{plantType.Name}'", ex);
            }
        }

        public PlantDefinition? GetDefinition(string typeName)
        {
            return _definitions.FirstOrDefault(d => d.TypeName.Equals(typeName, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<PlantDefinition> GetAvailableDefinitions()
        {
            return _definitions;
        }
    }
}
