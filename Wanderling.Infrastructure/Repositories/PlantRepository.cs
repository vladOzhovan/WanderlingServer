using Microsoft.EntityFrameworkCore;
using Wanderling.Application.Models;
using Wanderling.Infrastructure.Data;
using Wanderling.Application.Interfaces;
using Wanderling.Infrastructure.Mappers;

namespace Wanderling.Infrastructure.Repositories
{
    public class PlantRepository : IPlantRepository
    {
        private readonly AppDbContext _context;

        public PlantRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserPlantModel>> GetByUserIdAsync(Guid userId)
        {
            var plantEntities = await _context.UserPlants.Where(p =>  p.UserId == userId).ToListAsync();
            var plantModels = plantEntities.Select(p => p.ToModel()).ToList();
            return plantModels;
        }

        public async Task AddToUserAsync(UserPlantModel model)
        {
            await _context.UserPlants.AddAsync(model.ToEntity());
            await _context.SaveChangesAsync();
        }

        public async Task AddToAll(CommonPlamtModel model)
        {
            await _context.AllPlants.AddAsync(model.ToEntity());
            await _context.SaveChangesAsync();
        }
    }
}
