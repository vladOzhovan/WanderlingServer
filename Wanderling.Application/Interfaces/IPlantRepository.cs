using Wanderling.Application.Models;

namespace Wanderling.Application.Interfaces
{
    public interface IPlantRepository
    {
        Task AddToUserAsync(UserPlantModel model);
        Task AddToAll(CommonPlamtModel model);
        Task<List<UserPlantModel>> GetByUserIdAsync(Guid userId);
    }
}
