using BarberSuite.Application.Contracts.ShopContracts;
using System.Data;

namespace BarberSuite.Application.Contracts
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        IShopRepository ShopRepository { get; } // Fixed PascalCase
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    }
}
