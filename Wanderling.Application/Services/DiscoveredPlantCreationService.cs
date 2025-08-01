using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Wanderling.Application.Interfaces;
using Wanderling.Application.Mappers;
using Wanderling.Application.Models;
using Wanderling.Domain.Entities;
using Wanderling.Domain.Entities.Collections.Plants;

namespace Wanderling.Application.Services
{
    public class DiscoveredPlantCreationService : IDiscoveredPlantCreationService
    {
        private readonly IPlantRepository _repository;
        private readonly IPlantCreationService _creationService;
        private readonly IPlantRecognitionService _recognitionService;
        private readonly ILogger<DiscoveredPlantCreationService> _logger;

        public DiscoveredPlantCreationService(
            ILogger<DiscoveredPlantCreationService> logger,
            IPlantRepository repository,
            IPlantCreationService creationService,
            IPlantRecognitionService recognitionService)
        {
            _logger = logger;
            _repository = repository;
            _creationService = creationService;
            _recognitionService = recognitionService;
        }

        public async Task<PlantIdentifiedModel> CreateDiscoveredAsync(IFormFile image, Guid userId)
        {
            using var ms = new MemoryStream();
            await image.CopyToAsync(ms);
            var imageBytes = ms.ToArray();

            var apiResponseJson = await _recognitionService.IdentifyPlantAsync(imageBytes);

            PlantApiResponse? responseResult;

            try
            {
                responseResult = JsonSerializer.Deserialize<PlantApiResponse>(apiResponseJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (JsonException ex)
            {
                _logger.LogError($"Failed to deserialize {typeof(PlantApiResponse)}");
                throw new InvalidOperationException("Plant recognition error", ex);
            }

            var plantScientificName = responseResult?.Suggestions?.FirstOrDefault()?.ScientificName;

            if (string.IsNullOrWhiteSpace(plantScientificName))
                throw new Exception("Unknown plant");

            var plantsRegisterPath = Path.Combine(AppContext.BaseDirectory, "Resources", "plantsRegister.json");
            
            if (!File.Exists(plantsRegisterPath))
                throw new Exception("plantsRegister.json not found");

            
            List<PlantDefinition>? register;

            try
            {
                var jsonReg = File.ReadAllText(plantsRegisterPath);

                register = JsonSerializer.Deserialize<List<PlantDefinition>>(jsonReg, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (IOException ex)
            {
                _logger.LogError(ex, "Failed to read plantsRegister.json");
                throw new FileNotFoundException("Error while reading plantsRegister.json", ex);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "plantsRegister.json has invalid format");
                throw new JsonException("Invalid plantsRegister.json format", ex);
            }

            var definition = register?.FirstOrDefault(p => 
                string.Equals(p.ScientificName, plantScientificName, StringComparison.OrdinalIgnoreCase));

            if (definition == null)
                throw new Exception($"Plant '{plantScientificName}' not found in registry");

            var model = new PlantCreateModel
            {
                SpeciesKey = plantScientificName,
                TypeKey = definition.TypeName,
                ReproductionKey = definition.Reproduction
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
