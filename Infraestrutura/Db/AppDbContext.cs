using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using minimal_api.Dominio.Entidades;

namespace minimal_api.Infraestrutura.Db
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configurationAppSettings;
        
        public AppDbContext(IConfiguration configurationAppSettings)
        {
            _configurationAppSettings = configurationAppSettings;
        }

        public DbSet<Administrador> Administradores { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (!Administradores.Any())
            {
                modelBuilder.Entity<Administrador>().HasData(
                    new Administrador
                    {
                        Id = 1,
                        Email = "admin",
                        Senha = "admin",
                        Perfil = "adm"
                    }
                );
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) { 
                var stringConnection = _configurationAppSettings.GetConnectionString("sqlserver");
                if (!stringConnection.IsNullOrEmpty()) optionsBuilder.UseSqlServer(stringConnection);
            }
        }
    }
}
