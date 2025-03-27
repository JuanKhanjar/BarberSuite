using BarberSuite.Domain.Models.Shops;
using BarberSuite.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using DayOfWeek = BarberSuite.Domain.Models.DayOfWeek;
namespace BarberSuite.Infrastructure.Persistence.EntityConfigurations
{
    public class ShopConfiguration : IEntityTypeConfiguration<Shop>
    {
        public void Configure(EntityTypeBuilder<Shop> builder)
        {
            // Table name
            builder.ToTable("Shops");

            // Primary Key
            builder.HasKey(s => s.Id);

            // CVR Configuration
            builder.Property(s => s.Cvr)
                .HasMaxLength(8)
                .IsRequired()
                .IsFixedLength();

            // Unique Index for CVR
            builder.HasIndex(s => s.Cvr)
                .IsUnique()
                 .HasDatabaseName("IX_Shops_Cvr");

            // Name Configuration
            builder.Property(s => s.Name)
                .HasMaxLength(100)
                .IsRequired();

            // Address (Owned Entity)
            // Existing configurations
            builder.OwnsOne(s => s.Address);

            // Phone Configuration
            builder.Property(s => s.Phone)
                .HasMaxLength(8)
                .IsFixedLength();

            // Opening Hours JSON Conversion
            builder.Property(s => s.OpeningHours)
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, JsonSerializationOptions.Default),
                        v => JsonSerializer.Deserialize<Dictionary<DayOfWeek, (TimeSpan, TimeSpan)>>(
                            v, JsonSerializationOptions.Default)!);

            // Services Relationship
            builder.HasMany(s => s.Services)
                .WithOne()
                .HasForeignKey(s => s.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
