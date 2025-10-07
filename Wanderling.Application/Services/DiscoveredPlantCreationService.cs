using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Wanderling.Application.Interfaces;
using Wanderling.Application.Mappers;
using Wanderling.Application.Models;
using Wanderling.Domain.Entities;
using Wanderling.Domain.Entities.Collections.Plants;

namespace Wanderling.Application.Services
{
    /// <summary>
    /// Provides functionality for processing uploaded plant images, identifying plants using a recognition service, 
    /// and creating corresponding plant models in the system.
    /// </summary>
    /// <remarks>This service integrates with a plant recognition API to identify plants based on uploaded
    /// images.  It validates the identified plant against a predefined registry and creates a plant entity if the plant
    /// is recognized. The service also associates the created plant with a user.</remarks>
    public class DiscoveredPlantCreationService : IDiscoveredPlantCreationService
    {
        private readonly IMemoryCache _cache;
        private readonly IPlantRepository _repository;
        private readonly IPlantCreationService _creationService;
        private readonly IPlantRecognitionService _recognitionService;
        private readonly ILogger<DiscoveredPlantCreationService> _logger;

        public DiscoveredPlantCreationService(
            IMemoryCache cache,
            ILogger<DiscoveredPlantCreationService> logger,
            IPlantRepository repository,
            IPlantCreationService creationService,
            IPlantRecognitionService recognitionService)
        {
            _cache = cache;
            _logger = logger;
            _repository = repository;
            _creationService = creationService;
            _recognitionService = recognitionService;
        }

        private const string PlantsRegisterCacheKey = "PlantsRegister";

        /// <summary>
        /// Loads and returns the plant registry from plantsRegister.json
        /// </summary>
        /// <returns>A Lists of <see cref="PlantDefinition"/> objects loaded from the registry</returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        private List<PlantDefinition>? GetPlantsRegister()
        {
            var plantsReg = _cache.GetOrCreate(PlantsRegisterCacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);

                var path = Path.Combine(AppContext.BaseDirectory, "Resources", "plantsRegister.json");

                try
                {
                    if (!File.Exists(path))
                        throw new FileNotFoundException("plantsRegister.json not found");

                    var json = File.ReadAllText(path);

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var result = JsonSerializer.Deserialize<List<PlantDefinition>>(json, options);

                    if (result == null || result.Count == 0)
                        throw new InvalidOperationException("plantsRegister.json is empty or invalid");

                    return result;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to load plantsRegister.json");
                    throw;
                }
            });

            return plantsReg;
        }
        /// <summary>
        /// Processes an uploaded image, sends it to the recognition service and returns an identified plant model 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="userId"></param>
        /// <returns cref="PlantIdentifiedModel"></returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<PlantIdentifiedModel> CreateDiscoveredAsync(IFormFile image, Guid userId)
        {
            using var memoryStream = new MemoryStream();
            await image.CopyToAsync(memoryStream);
            var imageBytes = memoryStream.ToArray();

            var apiResponseJson = await _recognitionService.IdentifyPlantAsync(imageBytes);

            PlantApiResponse? responseResult;

            try
            {
                // Deserialize the response we got from the IPlantRecognitionService
                responseResult = JsonSerializer.Deserialize<PlantApiResponse>(apiResponseJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, $"Failed to deserialize {typeof(PlantApiResponse)}");
                throw new InvalidOperationException("Plant recognition error", ex);
            }

            var plantScientificName = responseResult?.Suggestions?.FirstOrDefault()?.ScientificName;

            if (string.IsNullOrWhiteSpace(plantScientificName))
                throw new Exception("Unknown plant");

            // Forming a register from plantsRegister.json
            var register = GetPlantsRegister();

            // Getting Plant definition from the register
            var definition = register?.FirstOrDefault(p => 
                string.Equals(p.ScientificName, plantScientificName, StringComparison.OrdinalIgnoreCase));

            if (definition == null)
                throw new Exception($"Plant '{plantScientificName}' not found in registry");

            // Creating a Plant model for the IPlantCreationService
            var model = new PlantCreateModel
            {
                SpeciesKey = plantScientificName,
                TypeKey = definition.TypeName,
                ReproductionKey = definition.Reproduction
            };

            // Creating a Plant Domain entity
            var plant = await _creationService.CreatePlantAsync(model) as Plant;

            if (plant != null)
                plant.Discover(userId);
            else
                throw new Exception("Failed to create plant");

            var identifiedPlant = plant.ToPlantIdentifiedModel();

            if (identifiedPlant == null)
                throw new Exception($"Failed to convert from {typeof(Plant)} to {typeof(PlantIdentifiedModel)}");

            var userPlantModel = PlantMapper.ToUserPlantModel(plant);

            if (userPlantModel == null)
                throw new Exception($"Failed to convert from {typeof(PlantIdentifiedModel)} to {typeof(UserPlantModel)}");

            await _repository.AddToUserAsync(userPlantModel);

            return identifiedPlant;
        }
    }
}
