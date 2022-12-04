using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace CarRent.Entites.FluentApi
{

    internal class CustomerBuilder : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {

            builder.HasMany(c => c.Cars).WithMany(cs => cs.Customers).UsingEntity<Rent>(
                c => c.HasOne(r => r.Car).WithMany().HasForeignKey(r => r.CarId),
                c => c.HasOne(r => r.Customer).WithMany().HasForeignKey(r => r.CustomerId),
                r =>
                {
                    r.HasKey(x => new { x.CarId, x.CustomerId });
                });

        }
    }
}
