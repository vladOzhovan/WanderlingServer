using System.Reflection;
using Wanderling.Domain.Entities;
using Wanderling.Domain.Attributes;
using Wanderling.Infrastructure.Configuration;
using Wanderling.Domain.Interfaces;
using System.Text.Json;

namespace Wanderling.Infrastructure.Startup
{
    public static class OrganismConfigGenerator
    {
        public static void GenerateConfig(string outPutPath)
        {
            var assembly = typeof(Organism).Assembly;

            var organismTypes = assembly
                .GetTypes()
                .Where(
                    t => !t.IsAbstract &&
                    typeof(Organism).IsAssignableFrom(t) &&
                    t.GetCustomAttribute<OrganismMetadataAttribute>() != null)
                .ToList();

            var configList = new List<OrganismConfig>();

            foreach (var type in organismTypes)
            {
                var attribute = type.GetCustomAttribute<OrganismMetadataAttribute>();
                if ( attribute == null ) continue;

                var strategyType = attribute.StrategyType;

                configList.Add(new OrganismConfig
                {
                    Type = attribute.TypeKey,
                    Assembly = type.Assembly.GetName().Name ?? string.Empty,
                    Class = type.FullName ?? string.Empty,
                    Strategy = strategyType.FullName ?? string.Empty,
                });

                var json = JsonSerializer.Serialize(configList, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                File.WriteAllText(outPutPath, json);
            }
        }
    }
}
