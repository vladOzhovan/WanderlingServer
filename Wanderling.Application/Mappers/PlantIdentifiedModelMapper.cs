using Wanderling.Application.Models;
using Wanderling.Domain.Entities.Collections.Plants;

namespace Wanderling.Application.Mappers
{
    public static class PlantIdentifiedModelMapper
    {
        public static PlantIdentifiedModel ToPlantIdentifiedModel(this Plant plant)
        {
            return new PlantIdentifiedModel
            {
                Id = plant.Id,
                UserId = plant.UserId ?? Guid.Empty,
                SpeciesName = plant.SpeciesName,
                DisplayedName = plant.DisplayedName,
                Description = plant.Description,
                Reproduction = plant.Reproduction,
                ImageUrl = plant.ImageUrl,
                DiscoverededAt = DateTime.UtcNow
            };
        }
    }
}
