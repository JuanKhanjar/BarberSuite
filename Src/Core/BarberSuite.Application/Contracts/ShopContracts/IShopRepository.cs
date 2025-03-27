using BarberSuite.Domain.Models.Shops;

namespace BarberSuite.Application.Contracts.ShopContracts
{
    public interface IShopRepository : IGenericRepository<Shop>
    {
        Task<IReadOnlyList<Shop>> SearchAsync(ShopSearchCriteria criteria);
    }
}
