using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Wanderling.Infrastructure.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "Wanderling.Api"));
            var configPath = Path.Combine(basePath, "appsettings.Development.json");

            if (!File.Exists(configPath))
                throw new FileNotFoundException($"Config file not found at: {configPath}");

            Console.WriteLine($"Using config: {configPath}");

            var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.Development.json", optional: false)
            .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlite(configuration.GetConnectionString("SqliteConnection"),
                b => b.MigrationsAssembly("Wanderling.Infrastructure"));

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
