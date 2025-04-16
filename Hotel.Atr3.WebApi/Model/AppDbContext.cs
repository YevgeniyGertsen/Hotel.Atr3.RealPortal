using Microsoft.EntityFrameworkCore;

namespace Hotel.Atr3.WebApi.Model
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options){ }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Position> Positions { get; set; }
    }
}