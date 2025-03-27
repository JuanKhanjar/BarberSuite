using BarberSuite.Application.Contracts.ShopContracts;
using BarberSuite.Domain.Models.Shops;
using Microsoft.EntityFrameworkCore;

namespace BarberSuite.Infrastructure.Persistence.Repositories.ShopsRepo
{
    public class ShopRepository : GenericRepository<Shop>, IShopRepository
    {
        public ShopRepository(DbContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Shop>> SearchAsync(ShopSearchCriteria criteria)
        {
            var query = _context.Set<Shop>().AsQueryable();

            // Only apply filters if criteria is provided
            if (criteria != null)
            {
                if (!string.IsNullOrWhiteSpace(criteria.Cvr))
                    query = query.Where(s => s.Cvr == criteria.Cvr);

                if (!string.IsNullOrWhiteSpace(criteria.Name))
                    query = query.Where(s => s.Name.Contains(criteria.Name));

                if (!string.IsNullOrWhiteSpace(criteria.City))
                    query = query.Where(s => s.Address.City == criteria.City);
            }

            return await query.AsNoTracking().ToListAsync();
        }
    }
}
