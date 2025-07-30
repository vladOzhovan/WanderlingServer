using Microsoft.EntityFrameworkCore;
using Wanderling.Application.Interfaces;
using Wanderling.Application.Models;
using Wanderling.Infrastructure.Data;
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

        public async Task<List<PlantModel>> GetByUserIdAsync(Guid userId)
        {
            var plantEntities = await _context.UserPlants.Where(p =>  p.UserId == userId).ToListAsync();
            var plantModels = plantEntities.Select(p => p.ToModel()).ToList();
            return plantModels;
        }

        public async Task AddAsync(PlantModel model)
        {
            await _context.UserPlants.AddAsync(model.ToEntity());
            await _context.SaveChangesAsync();
        }
    }
}
