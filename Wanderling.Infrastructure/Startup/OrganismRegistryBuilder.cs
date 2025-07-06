using Wanderling.Infrastructure.Configuration;

namespace Wanderling.Infrastructure.Startup
{
    public class OrganismRegistryBuilder
    {
        private readonly IDictionary<string, OrganismConfig> _map;

        public OrganismRegistryBuilder(IEnumerable<OrganismConfig> configs)
        {
            _map = configs.ToDictionary(c => c.Type.ToLower(), c => c);
        }

        public IReadOnlyCollection<string> GetSupportedTypes()
            => _map.Keys.ToList().AsReadOnly();

        public bool IsSupported(string typeKey)
            => _map.ContainsKey(typeKey.ToLower());

        public OrganismConfig GetConfig(string typeKey)
            => _map.TryGetValue(typeKey.ToLower(), out var config)
            ? config
            : throw new ArgumentException($"Unknown type '{typeKey}'");
    }
}
