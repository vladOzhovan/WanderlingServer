using System.Reflection;
using Wanderling.Application.Models;
using Wanderling.Domain.Attributes;
using Wanderling.Domain.Entities.Collections.Plants;

namespace Wanderling.Application.Mappers
{
    public static class PlantMapper
    {
        public static UserPlantModel ToModel(this Plant plant)
        {
            var type = plant.GetType();
            var metadata = type.GetCustomAttribute<PlantMetadataAttribute>();

            return new UserPlantModel
            {
                Id = plant.Id,
                UserId = plant.UserId ?? Guid.Empty,
                ScientificName = plant.ScientificName ?? string.Empty,
                DisplayedName = plant.DisplayedName ?? string.Empty,
                TypeName = metadata?.TypeKey ?? type.Name,
                Description = plant.Description ?? string.Empty,
                Reproduction = plant.Reproduction ?? string.Empty,
                Rarity = plant.Rarity ?? string.Empty,
                ImageUrl = plant.ImageUrl ?? string.Empty,
                Effects = plant.Effects ?? new(),
                CreatedAt = plant.CreatedAt,
                DiscoveredAt = plant.DiscoverededAt ?? DateTime.UtcNow
            };
        }
    }
}
