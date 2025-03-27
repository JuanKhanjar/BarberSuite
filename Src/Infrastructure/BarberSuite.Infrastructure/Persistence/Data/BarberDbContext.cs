using BarberSuite.Domain.Models;
using BarberSuite.Domain.Models.Shops;
using Microsoft.EntityFrameworkCore;


namespace BarberSuite.Infrastructure.Persistence.Data
{
    // BarberDbContext.cs
    public class BarberDbContext : DbContext
    {
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply all configurations in this assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BarberDbContext).Assembly);
        }

    }
}
