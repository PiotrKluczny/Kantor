using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kantor.Models
{
    //polaczenie sie z baza danych
    public class NbpDbContext : DbContext
    {
        public DbSet<NbpCurrency> NbpCurrencys { get; set; }
        public DbSet<NbpCurrencyDictionare> NbpCurrencyDictionares { get; set; }

        //tworzymy baze danych ale w pliku. 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=MyDatabase.db");
        }
    }
}

