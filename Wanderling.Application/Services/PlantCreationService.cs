using Wanderling.Application.Interfaces;
using Wanderling.Application.Mappers;
using Wanderling.Application.Models;
using Wanderling.Domain.Interfaces;
using Wanderling.Domain.Strategies;

namespace Wanderling.Application.Services
{
    public class PlantCreationService : IPlantCreationService
    {
        private readonly IOrganismFactory _factory;
        private readonly IPlantRepository _repository;
        public PlantCreationService(IOrganismFactory factory, IPlantRepository repository)
        {
            _factory = factory;
            _repository = repository;
        }

        public async Task<IOrganism> CreatePlantAsync(PlantCreateModel model)
        {
            IReproduction reproduction = model.ReproductionKey.ToLower() switch
            {
                "seed" => new SeedReproduction(),
                "spore" => new SporeReproduction(),
                "sexual" => new SexualReproduction(),
                _ => throw new ArgumentException($"Unknown reproduction: {model.ReproductionKey}")
            };

            var plant = _factory.Create(model.Name, model.TypeKey, reproduction);
            await _repository.AddAsync(plant.ToModel());
            return await Task.FromResult(plant);
        }
    }
}
