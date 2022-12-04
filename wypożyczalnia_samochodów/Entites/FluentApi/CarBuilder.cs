using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace CarRent.Entites.FluentApi
{
    internal class CarBuilder : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {

            builder.Property(wi => wi.IsCar).HasDefaultValue(true);
            builder.Property(wi => wi.Name).IsRequired().HasMaxLength(20);
            builder.Property(wi => wi.Combustion).IsRequired();
            builder.Property(wi => wi.Localization).IsRequired();
            builder.Property(wi => wi.Class).IsRequired();

        }
    }
}
