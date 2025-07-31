using System.Reflection;
using Wanderling.Domain.Attributes;
using Wanderling.Application.Models;
using Wanderling.Domain.Entities.Collections.Plants;

namespace Wanderling.Application.Mappers
{
    public static class PlantDtoModelMapper
    {
        public static PlantDto ToPlantDto(this Plant plant)
        {
            var type = plant.GetType();
            var metadata = type.GetCustomAttribute<PlantMetadataAttribute>();

            return new PlantDto
            {
                Id = plant.Id,
                UserId = plant.UserId ?? Guid.Empty,
                SpeciesName = plant.SpeciesName ?? string.Empty,
                DisplayedName = plant.DisplayedName ?? string.Empty,
                Type = metadata?.TypeKey ?? type.Name,
                Reproduction = plant.Reproduction ?? string.Empty,
                Effects = plant.Effects,
                Rarity = plant.Rarity ?? string.Empty,
                CreatedAt = plant.CreatedAt
            };
        }

        public static UserPlantModel ToPlantModel(this Plant plant)
        {
            var type = plant.GetType();
            var metadata = type.GetCustomAttribute<PlantMetadataAttribute>();

            return new UserPlantModel
            {
                Id = plant.Id,
                UserId = plant.UserId ?? Guid.Empty,
                SpeciesName = plant.SpeciesName,
                DisplayedName = plant.DisplayedName,
                TypeName = metadata?.TypeKey ?? type.Name,
                Reproduction = plant.Reproduction,
                Description = plant.Description,
                Rarity = plant.Rarity,
                Effects = plant.Effects,
                CreatedAt = plant.CreatedAt
            };
        }
    }
}
