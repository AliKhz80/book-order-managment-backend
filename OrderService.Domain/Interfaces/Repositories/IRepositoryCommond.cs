using OrderService.Domain.Entities.BaseEntity;
using System.Numerics;

namespace OrderService.Domain.Interfaces.Repositories;

public interface IRepositoryCommond<TEntity, T> : IRepositoryQuery<TEntity, T> where TEntity : Entity<T> where T : INumber<T>
{
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}
