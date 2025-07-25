using Wanderling.Application.Models;
using Wanderling.Infrastructure.Entities;

namespace Wanderling.Infrastructure.Mappers
{
    public static class PlantMapper
    {
        public static PlantEntity ToEntity(this PlantModel model)
        {
            return new PlantEntity
            {
                Id = model.Id,
                UserId = model.UserId,
                SpeciesName = model.SpeciesName,
                DisplayedName = model.DisplayedName,
                Type = model.TypeName,
                Reproduction = model.Reproduction,
                Description = model.Description,
                Rarity = model.Rarity,
                Effects = model.Effects,
                CreatedAt = model.CreatedAt
            };
        }

        public static PlantModel ToModel(this PlantEntity entity)
        {
            return new PlantModel
            {
                Id = entity.Id,
                UserId = entity.UserId,
                SpeciesName = entity.SpeciesName,
                DisplayedName = entity.DisplayedName,
                TypeName = entity.Type,
                Reproduction = entity.Reproduction,
                Description = entity.Description,
                Rarity = entity.Rarity,
                Effects = entity.Effects,
                CreatedAt = entity.CreatedAt
            };
        }
    }
}
