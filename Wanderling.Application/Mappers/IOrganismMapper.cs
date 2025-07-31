using System.Reflection;
using Wanderling.Domain.Attributes;
using Wanderling.Domain.Interfaces;
using Wanderling.Application.Models;

namespace Wanderling.Application.Mappers
{
    public static class IOrganismMapper
    {
        public static UserPlantModel ToModel(this IOrganism organism)
        {
            var type = organism.GetType();
            var metadata = type.GetCustomAttribute<PlantMetadataAttribute>();

            return new UserPlantModel
            {
                Id = organism.Id,
                UserId = organism.UserId ?? Guid.Empty,
                SpeciesName = organism.SpeciesName ?? string.Empty,
                DisplayedName = organism.DisplayedName ?? string.Empty,
                TypeName = metadata.TypeKey ?? type.Name,
                Reproduction = organism.Reproduction ?? string.Empty,
                Description = organism.Description ?? string.Empty,
                CreatedAt = organism.CreatedAt,
                DiscoveredAt = organism.DiscoverededAt ?? DateTime.UtcNow
            };
        }
    }
}
