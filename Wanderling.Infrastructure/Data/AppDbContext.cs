using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Wanderling.Infrastructure.Entities;
using Wanderling.Domain.Entities.Collections.Plants;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Wanderling.Domain.Entities.Collections.Funguses;

namespace Wanderling.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<UserEntity, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<PlantEntity> Plants {  get; set; }
        public DbSet<UserEntity> Users {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PlantEntity>().HasKey(p => p.Id);

            modelBuilder.Ignore<Plant>();

            var plantTypes = Assembly.GetAssembly(typeof(Plant)).GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Plant)) && !t.IsAbstract);

            foreach (var type in  plantTypes)
                modelBuilder.Ignore(type);

            var fungusTypes = Assembly.GetAssembly(typeof(Fungus)).GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Fungus)) && !t.IsAbstract);

            foreach (var type in fungusTypes)
                modelBuilder.Ignore(type);
        }
    }
}
