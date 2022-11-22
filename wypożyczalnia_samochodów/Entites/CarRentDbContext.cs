using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Entites
{
    public class CarRentDbContext : DbContext
    {
        private string _connectionString =
            "Server=DESKTOP-HR4PVP4;Database=CarRentDb;Trusted_Connection=True;Encrypt=False;";
        public DbSet<CarRental> CarRents { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Rent> Rents { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(eb =>
            {
                eb.HasMany(e => e.customers).WithOne(c => c.employee).HasForeignKey(c => c.employeeId);
                eb.Property(wi => wi.phone).IsRequired().HasMaxLength(9);
            });
            modelBuilder.Entity<Customer>(eb =>
            {
                eb.HasMany(c => c.cars).WithMany(cs => cs.customers).UsingEntity<Rent>(
                    c => c.HasOne(r => r.car).WithMany().HasForeignKey(r => r.carId),
                    c => c.HasOne(r => r.customer).WithMany().HasForeignKey(r => r.customerId),
                    r =>
                    {
                        r.HasKey(x => new { x.carId, x.customerId });

                    });

            });



            modelBuilder.Entity<Car>(eb =>
            {
                eb.Property(wi => wi.isCar).HasDefaultValue(true);
                eb.Property(wi => wi.name).IsRequired().HasMaxLength(20);
                eb.Property(wi => wi.combustion).IsRequired();
                eb.Property(wi => wi.localization).IsRequired();
                eb.Property(wi => wi.Class).IsRequired();
            });
            //modelBuilder.Entity<Rent>(eb =>
            //{
            //});
            //modelBuilder.Entity<CarRental>(eb =>
            //{
            //    eb.HasMany(cr => cr.cars).WithOne(c => c.carRent).HasForeignKey(c => c.carRentId);
            //    eb.HasMany(cr => cr.employees).WithOne(e => e.carRental).HasForeignKey(e => e.carRentalId);

            //});



        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

    }
}
