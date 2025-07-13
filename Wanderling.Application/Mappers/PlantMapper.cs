using System.Reflection;
using Wanderling.Domain.Attributes;
using Wanderling.Domain.Interfaces;
using Wanderling.Application.Models;

namespace Wanderling.Application.Mappers
{
    public static class PlantMapper
    {
        public static PlantModel ToModel(this IOrganism organism)
        {
            var type = organism.GetType();
            var metadata = type.GetCustomAttribute<PlantMetadataAttribute>();

            return new PlantModel
            {
                Id = organism.Id,
                UserId = organism.UserId ?? null,
                Name = organism.Name,
                Type = metadata.TypeKey ?? type.Name,
                Reproduction = organism.Reproduction,
                CreatedAt = organism.CreatedAt
            };
        }
    }
}
