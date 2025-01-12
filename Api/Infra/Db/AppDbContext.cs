using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.IdentityModel.Tokens;
using minimal_api.Domain.Entities;

namespace minimal_api.Infra.Db
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configurationAppSettings;

        public AppDbContext(IConfiguration configurationAppSettings)
        {
            _configurationAppSettings = configurationAppSettings;
        }

        public DbSet<Admin> Admins { get; set; } = default!;
        public DbSet<Veicle> Veicles { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    Id = 1,
                    Email = "admin",
                    Password = "admin",
                    Role = "Adm"
                }
            );

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var stringConnection = _configurationAppSettings.GetConnectionString("SqlServer");
                if (!string.IsNullOrEmpty(stringConnection)) optionsBuilder.UseSqlServer(stringConnection);
            }
        }
    }
}
