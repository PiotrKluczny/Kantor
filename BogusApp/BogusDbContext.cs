using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;


namespace BogusApp
{
    class BogusDbContext : DbContext
    {
        public BogusDbContext()
        {
            Database.EnsureCreated();
        }
        public DbSet<Person> Persons { get; set; }

        private readonly string _filePath;

        public BogusDbContext(FilePath filePath)
        {
            _filePath = filePath.PathDb;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_filePath}");
        }
    }
}
