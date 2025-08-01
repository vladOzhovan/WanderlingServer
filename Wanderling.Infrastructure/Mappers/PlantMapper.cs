using Wanderling.Application.Models;
using Wanderling.Infrastructure.Entities;

namespace Wanderling.Infrastructure.Mappers
{
    public static class PlantMapper
    {
        public static UserPlantEntity ToEntity(this UserPlantModel model)
        {
            return new UserPlantEntity
            {
                Id = model.Id,
                UserId = model.UserId,
                ScientificName = model.ScientificName,
                DisplayedName = model.DisplayedName,
                Type = model.TypeName,
                Reproduction = model.Reproduction,
                Description = model.Description,
                Rarity = model.Rarity,
                Effects = model.Effects,
                CreatedAt = model.CreatedAt
            };
        }

        public static UserPlantModel ToModel(this UserPlantEntity entity)
        {
            return new UserPlantModel
            {
                Id = entity.Id,
                UserId = entity.UserId,
                ScientificName = entity.ScientificName,
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
