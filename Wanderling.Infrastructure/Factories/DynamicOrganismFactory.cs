using System.Reflection;
using Wanderling.Domain.Entities;
using Wanderling.Domain.Interfaces;
using Wanderling.Infrastructure.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Wanderling.Infrastructure.Factories
{
    public class DynamicOrganismFactory : IOrganismFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<string, OrganismConfig> _configMap;

        public DynamicOrganismFactory(IEnumerable<OrganismConfig> configs, IServiceProvider serviceProvider)
        {
            _configMap = configs.ToDictionary(c => c.Type.ToLower());
            _serviceProvider = serviceProvider;
        }

        public Organism Create(string typeKey, string name)
        {
            if (!_configMap.TryGetValue(typeKey.ToLower(), out var config))
                throw new ArgumentException($"Unknown organism type: {typeKey}");

            // Load strategies
            var assembly = Assembly.Load(config.Assembly);

            var strategyType = assembly.GetType(config.Strategy)
                ?? throw new InvalidOperationException($"Strategy class '{config.Strategy}' not found");

            var strategy = (IReproductionStrategy)ActivatorUtilities.CreateInstance(_serviceProvider, strategyType);

            // Load organisms
            var organismType = assembly.GetType(config.Class)
                ?? throw new InvalidOperationException($"Organism class '{config.Class}' not found");

            var organism = (Organism)ActivatorUtilities.CreateInstance(_serviceProvider, organismType, strategy);

            organism.Name = name;
            organism.CreatedAt = DateTime.UtcNow;
            
            return organism;
        }

    }
}
