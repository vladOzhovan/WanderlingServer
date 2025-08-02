using System.Reflection;
using Wanderling.Application.Models;
using Wanderling.Domain.Attributes;
using Wanderling.Domain.Entities.Collections.Plants;

namespace Wanderling.Application.Mappers
{
    public static class PlantIdentifiedModelMapper
    {
        public static PlantIdentifiedModel ToPlantIdentifiedModel(this Plant plant)
        {
            var type = plant.GetType();
            var metadata = type.GetCustomAttribute<PlantMetadataAttribute>();

            return new PlantIdentifiedModel
            {
                Id = plant.Id,
                UserId = plant.UserId ?? Guid.Empty,
                ScientificName = plant.ScientificName,
                DisplayedName = plant.DisplayedName,
                TypeName = metadata?.TypeKey ?? type.Name,
                Description = plant.Description,
                Reproduction = plant.Reproduction,
                Rarity = plant.Rarity,
                ImageUrl = plant.ImageUrl,
                CreatedAt = plant.CreatedAt,
                DiscoverededAt = plant.DiscoverededAt ?? DateTime.UtcNow
            };
        }
    }
}
