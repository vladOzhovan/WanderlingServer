using Microsoft.AspNetCore.Http;
using Wanderling.Application.Models;

namespace Wanderling.Application.Interfaces
{
    public interface IDiscoveredPlantCreationService
    {
        Task<PlantIdentifiedModel> CreateDiscoveredAsync(IFormFile image, Guid userId);
    }
}
