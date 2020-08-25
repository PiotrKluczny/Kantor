using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace FindMyHouse
{
    class FindMyHouseDbContext : DbContext
    {
        public FindMyHouseDbContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<Street> Streets { get; set; }
        public DbSet<Flat> Flats { get; set; }
        public DbSet<Resident> Residents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename=C:\\Database\\MyHouse.db");
        }
    }
}
