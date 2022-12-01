using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRent.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CarRent.DbContexts
{
    public class CarRentDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public CarRentDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public DbSet<CarRental> CarRents { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Rent> Rents { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(eb =>
            {
                eb.HasMany(e => e.Customers).WithOne(c => c.Employee).HasForeignKey(c => c.EmployeeId);
                eb.Property(wi => wi.Phone).IsRequired().HasMaxLength(9);
            });
            modelBuilder.Entity<Customer>(eb =>
            {
                eb.HasMany(c => c.Cars).WithMany(cs => cs.Customers).UsingEntity<Rent>(
                    c => c.HasOne(r => r.Car).WithMany().HasForeignKey(r => r.CarId),
                    c => c.HasOne(r => r.Customer).WithMany().HasForeignKey(r => r.CustomerId),
                    r =>
                    {
                        r.HasKey(x => new { x.CarId, x.CustomerId });
                    });
            });
            modelBuilder.Entity<Car>(eb =>
            {
                eb.Property(wi => wi.IsCar).HasDefaultValue(true);
                eb.Property(wi => wi.Name).IsRequired().HasMaxLength(20);
                eb.Property(wi => wi.Combustion).IsRequired();
                eb.Property(wi => wi.Localization).IsRequired();
                eb.Property(wi => wi.Class).IsRequired();
            });
            //modelBuilder.Entity<Rent>(eb =>
            //{
            //});
            //modelBuilder.Entity<CarRental>(eb =>
            //{
            //    eb.HasMany(cr => cr.Cars).WithOne(c => c.carRent).HasForeignKey(c => c.carRentId);
            //    eb.HasMany(cr => cr.Employees).WithOne(e => e.carRental).HasForeignKey(e => e.carRentalId);

            //});



        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("CarRentDbContextString"));
        }

    }
}
