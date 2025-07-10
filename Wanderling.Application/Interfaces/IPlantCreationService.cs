using Wanderling.Application.Models;
using Wanderling.Domain.Interfaces;

namespace Wanderling.Application.Interfaces
{
    public interface IPlantCreationService
    {
        Task<IOrganism> CreatePlantAsync(PlantCreateModel model);
    }
}
