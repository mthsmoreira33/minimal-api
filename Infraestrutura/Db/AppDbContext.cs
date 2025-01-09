using Microsoft.EntityFrameworkCore;

namespace minimal_api.Infraestrutura.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
    }
}
