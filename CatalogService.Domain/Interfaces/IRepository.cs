using CatalogService.Domain.ModelConfigs;
using System.Numerics;

namespace CatalogService.Domain.Interfaces
{
    public interface IRepository<TEntity,T> : IReadOnlyRepository<TEntity,T> where TEntity : Entity<T> where T : INumber<T>
    {
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    }
}
