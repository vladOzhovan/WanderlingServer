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
        
        public DbSet<UserPlantEntity> UserPlants {  get; set; }
        public DbSet<UserEntity> Users {  get; set; }
        public DbSet<PlantEntity> AllPlants {  get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<IdentityRole<Guid>> roles = new List<IdentityRole<Guid>>
            {
                new IdentityRole<Guid>
                {
                    //Id = Guid.Parse("9a5f8dc6-1a26-4f27-b52e-ffed5573e178"),
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },

                new IdentityRole<Guid>
                {
                    //Id = Guid.Parse("1c1d88c6-6fbe-4d9c-932b-d2a0c2eb39f1"),
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "User",
                    NormalizedName = "USER"
                }
            };

            builder.Entity<IdentityRole<Guid>>().HasData(roles);

            builder.Entity<UserPlantEntity>().HasKey(p => p.Id);
            builder.Ignore<Plant>();

            var plantTypes = Assembly.GetAssembly(typeof(Plant)).GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Plant)) && !t.IsAbstract);

            foreach (var type in  plantTypes)
                builder.Ignore(type);

            var fungusTypes = Assembly.GetAssembly(typeof(Fungus)).GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Fungus)) && !t.IsAbstract);

            foreach (var type in fungusTypes)
                builder.Ignore(type);

            builder.Entity<UserPlantEntity>().OwnsMany(p => p.Effects, a =>
            {
                a.WithOwner().HasForeignKey("PlantId");
                a.Property<int>("Id");
                a.HasKey("Id");
            });

            builder.Entity<PlantEntity>().OwnsMany(p => p.Effects, a =>
            {
                a.WithOwner().HasForeignKey("PlantId");
                a.Property<int>("Id");
                a.HasKey("PlantId", "Key");
            });
        }
    }
}
