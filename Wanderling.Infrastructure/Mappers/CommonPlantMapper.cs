using System.Reflection;
using Wanderling.Domain.Attributes;
using Wanderling.Application.Models;
using Wanderling.Infrastructure.Entities;
using Wanderling.Domain.Entities.Collections.Plants;

namespace Wanderling.Infrastructure.Mappers
{
    public static class CommonPlantMapper
    {
        public static PlantEntity ToEntity(this CommonPlamtModel model)
        {
            return new PlantEntity
            {
                Id = model.Id,
                ScientificName = model.ScientificName,
                DisplayedName = model.DisplayedName,
                Type = model.Type,
                Reproduction = model.Reproduction,
                Description = model.Description,
                Rarity = model.Rarity,
                ImageUrl = model.ImageUrl,
                Effects= model.Effects,
                CreatedAt = model.CreatedAt,
            };
        }

        public static CommonPlamtModel ToCommonPlantModel(this Plant plant)
        {
            var type = plant.GetType();
            var metadata = type.GetCustomAttribute<PlantMetadataAttribute>();

            return new CommonPlamtModel
            {
                Id = plant.Id,
                ScientificName = plant.ScientificName,
                DisplayedName = plant.DisplayedName,
                Type = metadata?.TypeKey ?? type.Name,
                Description = plant.Description,
                Rarity = plant.Rarity,
                Reproduction = plant.Reproduction,
                ImageUrl = plant.ImageUrl,
                Effects = plant.Effects,
                CreatedAt = plant.CreatedAt
            };
        }
    }
}
