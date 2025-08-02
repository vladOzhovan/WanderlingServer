using Wanderling.Application.Interfaces;
using Wanderling.Application.Models;
using Wanderling.Domain.Entities.Collections.Plants;
using Wanderling.Domain.Interfaces;
using Wanderling.Domain.Reproduction;

namespace Wanderling.Application.Services
{
    public class PlantCreationService : IPlantCreationService
    {
        private readonly IOrganismFactory _plantFactory;
        public PlantCreationService(IOrganismFactory plantFactory)
        {
            _plantFactory = plantFactory;
        }

        public async Task<IOrganism> CreatePlantAsync(PlantCreateModel model)
        {
            IReproduction reproduction = model.ReproductionKey.ToLower() switch
            {
                "seed" => new SeedReproduction(),
                "spore" => new SporeReproduction(),
                "vegetative" or "veg" => new VegetativeReproduction(),
                _ => throw new ArgumentException($"Unknown reproduction: {model.ReproductionKey}")
            };

            var plant = _plantFactory.Create(model.SpeciesKey, model.TypeKey, reproduction) as Plant;

            if (plant == null)
                throw new Exception("Faild to create a Plant");

            return await Task.FromResult(plant);
        }
    }
}
