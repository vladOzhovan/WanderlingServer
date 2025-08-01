using System.Text.Json.Serialization;

namespace Wanderling.Application.Models
{
    public class PlantSuggestion
    {
        [JsonPropertyName("plant_name")]
        public string ScientificName { get; set; } = string.Empty;
    }
}
