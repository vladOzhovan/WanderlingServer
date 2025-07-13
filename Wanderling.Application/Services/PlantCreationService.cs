using Wanderling.Domain.Interfaces;
using Wanderling.Domain.Strategies;
using Wanderling.Application.Models;
using Wanderling.Application.Interfaces;

namespace Wanderling.Application.Services
{
    public class PlantCreationService : IPlantCreationService
    {
        private readonly IOrganismFactory _factory;
        public PlantCreationService(IOrganismFactory factory)
        {
            _factory = factory;
        }

        public Task<IOrganism> CreatePlantAsync(PlantCreateModel model)
        {
            IReproduction reproduction = model.ReproductionKey.ToLower() switch
            {
                "seed" => new SeedReproduction(),
                "spore" => new SporeReproduction(),
                "sexual" => new SexualReproduction(),
                _ => throw new ArgumentException($"Unknown reproduction: {model.ReproductionKey}")
            };

            var plant = _factory.Create(model.Name, model.TypeKey, reproduction);
            
            return Task.FromResult(plant);
        }
    }
}
