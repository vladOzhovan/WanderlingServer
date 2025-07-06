using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Wanderling.Infrastructure.Entities;
using Wanderling.Domain.Entities.Collections.Plants;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Wanderling.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<UserEntity, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<Plant> Plants {  get; set; }
    }
}
