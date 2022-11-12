using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Entites
{
    public class CarRentDbContext:DbContext
    {
        private string _connectionString =
            "Server=DESKTOP-EE18MCB;Database=CarRentDb2;Trusted_Connection=True;Encrypt=False;";
        public DbSet<CarRental> CarRents { get; set; }
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
