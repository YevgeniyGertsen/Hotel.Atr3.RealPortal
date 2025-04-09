using Microsoft.EntityFrameworkCore;

namespace Hotel.Atr3.Admin.Models
{
	public class AppDbContext : DbContext
	{
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options){}

        public DbSet<Position> Positions { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}