using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace wypożyczalnia_samochodów.Entites
{
    public class WypozyczalniaDbContext:DbContext
    {
        private string _connectionString =
            "Server=DESKTOP-EE18MCB;Database=WypozyczalniaDb2;Trusted_Connection=True;";
        public DbSet<Wypozyczalnia> Wypozyczalnie { get; set; }
        public DbSet<Car> Cars { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

    }
}
