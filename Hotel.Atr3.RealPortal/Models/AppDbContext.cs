﻿using Microsoft.EntityFrameworkCore;

namespace Hotel.Atr3.RealPortal.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {

        }

        public DbSet<Language> Language { get; set; }
        public DbSet<StringResources> StringResources { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Position> Positions { get; set; }
    }
}
