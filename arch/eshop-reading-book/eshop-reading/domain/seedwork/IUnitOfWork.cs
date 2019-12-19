using System;
using System.Threading;
using System.Threading.Tasks;

namespace eshop_reading.domain.seedwork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> SaveEntitiesAsync(CancellationToken cancelationToken = default(CancellationToken));
    }
}
