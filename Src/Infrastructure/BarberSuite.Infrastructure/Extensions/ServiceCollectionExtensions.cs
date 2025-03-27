using BarberSuite.Application.Contracts;
using BarberSuite.Application.Contracts.ShopContracts;
using BarberSuite.Infrastructure.Persistence.Data;
using BarberSuite.Infrastructure.Persistence.Repositories;
using BarberSuite.Infrastructure.Persistence.Repositories.ShopsRepo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarberSuite.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register DbContext
            services.AddDbContext<BarberDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Default")));
            // Register Repositories & UoW
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IShopRepository, ShopRepository>();

            return services;
        }
    }
}
