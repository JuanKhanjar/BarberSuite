using BarberSuite.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberSuite.Infrastructure.Persistence.EntityConfigurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("Services");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(s => s.BasePrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        }
    }
}
