using System.Text.Json;

namespace Wanderling.Infrastructure.Configuration
{
    public static class OrganismConfigLoader
    {
        public static List<OrganismConfig> LoadFromFile(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("Organism config file not found", path);

            var json = File.ReadAllText(path);

            var configs = JsonSerializer.Deserialize<List<OrganismConfig>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return configs ?? throw new InvalidOperationException("Invalid organism config file content");
        }
    }
}
