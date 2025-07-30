using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Wanderling.Application.Interfaces;
using Wanderling.Application.Models;

namespace Wanderling.Application.Services
{
    public class DiscoveredPlantCreationService : IDiscoveredPlantCreationService
    {
        private readonly IPlantCreationService _creationService;
        private readonly IPlantRecognitionService _recognitionService;
        public DiscoveredPlantCreationService(IPlantCreationService creationService, IPlantRecognitionService recognitionService)
        {
            _creationService = creationService;
            _recognitionService = recognitionService;
        }
        public async Task<PlantIdentifiedModel> CreateDiscoveredAsync(IFormFile image, Guid userId)
        {
            using var ms = new MemoryStream();
            await image.CopyToAsync(ms);
            var imageBytes = ms.ToArray();

            var json = await _recognitionService.IdentifyPlantAsync(imageBytes);

            var result = JsonSerializer.Deserialize<PlantApiResponse>(json);
            var plantName = result?.Suggestions?.FirstOrDefault()?.PlantName;


        }
    }
}
