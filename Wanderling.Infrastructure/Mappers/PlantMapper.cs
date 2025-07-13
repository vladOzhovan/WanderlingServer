using System.Reflection;
using Wanderling.Application.Models;
using Wanderling.Domain.Attributes;
using Wanderling.Domain.Interfaces;
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
                Name = model.Name,
                Type = model.Type,
                Reproduction = model.Reproduction,
                CreatedAt = model.CreatedAt
            };
        }

        public static PlantModel ToModel(this PlantEntity entity)
        {
            return new PlantModel
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Name = entity.Name,
                Type = entity.Type,
                Reproduction = entity.Reproduction,
                CreatedAt = entity.CreatedAt
            };
        }
    }
}
