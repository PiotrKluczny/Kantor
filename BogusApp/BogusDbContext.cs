using Microsoft.EntityFrameworkCore;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Filename=C:\Database\MyDatabaseBogus.db");
        }
    }
}
