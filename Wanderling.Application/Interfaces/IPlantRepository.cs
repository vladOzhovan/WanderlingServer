using Wanderling.Application.Models;

namespace Wanderling.Application.Interfaces
{
    public interface IPlantRepository
    {
        Task AddAsync(PlantModel model);
        Task<List<PlantModel>> GetByUserIdAsync(Guid userId);
    }
}
