using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Entites.FluentApi
{
    internal class EmployeeBuilder : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasMany(e => e.Customers).WithOne(c => c.Employee).HasForeignKey(c => c.EmployeeId);
            builder.Property(wi => wi.Phone).IsRequired().HasMaxLength(9);
        }
    }
}
