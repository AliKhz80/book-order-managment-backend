using CatalogService.Domain.Entities.BaseEntity;
using CatalogService.Domain.Specification;
using System.Numerics;

namespace CatalogService.Domain.Interfaces.Repositories
{
    public interface IRepositoryQuery<TEntity,T> where TEntity : Entity<T> where T : INumber<T>
    {
        Task<TEntity?> GetByIdAsync(T id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<(int TotalCount, IReadOnlyList<TEntity> Data)> ListAsync(Specification<TEntity> specification, CancellationToken cancellationToken = default);


    }

}
