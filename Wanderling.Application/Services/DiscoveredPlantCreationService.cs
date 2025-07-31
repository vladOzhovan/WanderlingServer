using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Wanderling.Application.Interfaces;
using Wanderling.Application.Mappers;
using Wanderling.Application.Models;
using Wanderling.Domain.Entities.Collections.Plants;

namespace Wanderling.Application.Services
{
    public class DiscoveredPlantCreationService : IDiscoveredPlantCreationService
    {
        private readonly IPlantCreationService _creationService;
        private readonly IPlantRecognitionService _recognitionService;
        private readonly IPlantRepository _repository;
        public DiscoveredPlantCreationService(
            IPlantCreationService creationService, IPlantRecognitionService recognitionService, IPlantRepository repository)
        {
            _repository = repository;
            _creationService = creationService;
            _recognitionService = recognitionService;
        }
        public async Task<PlantIdentifiedModel> CreateDiscoveredAsync(IFormFile image, Guid userId)
        {
            using var ms = new MemoryStream();
            await image.CopyToAsync(ms);
            var imageBytes = ms.ToArray();

            var json = await _recognitionService.IdentifyPlantAsync(imageBytes);

            var result = JsonSerializer.Deserialize<PlantApiResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var plantName = result?.Suggestions?.FirstOrDefault()?.PlantName;

            if (string.IsNullOrWhiteSpace(plantName))
                throw new Exception("Unknown plant");

            var model = new PlantCreateModel
            {
                SpeciesKey = plantName,
                TypeKey = "?",
                ReproductionKey = "?"
            };

            var plant = await _creationService.CreatePlantAsync(model) as Plant;

            if (plant == null)
                throw new Exception("Faild to create plant");

            var identifiedPlant = plant.ToPlantIdentifiedModel();

            if (identifiedPlant == null)
                throw new Exception($"Faild to convert from {typeof(Plant)} to {typeof(PlantIdentifiedModel)}");

            var userPlantModel = plant.ToModel();

            if (userPlantModel == null)
                throw new Exception($"Faild to convert from {typeof(PlantIdentifiedModel)} to {typeof(UserPlantModel)}");

            _repository.AddToUserAsync(userPlantModel);

            return identifiedPlant;
        }
    }
}
