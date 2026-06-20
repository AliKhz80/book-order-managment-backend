using CatalogService.Domain.ModelConfigs;
using CatalogService.Domain.SpecificationConfig;
using System.Numerics;

namespace CatalogService.Domain.Interfaces
{
    public interface IReadOnlyRepository<TEntity,T> where TEntity : Entity<T> where T : INumber<T>
    {
        Task<TEntity?> GetByIdAsync(T id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<(int TotalCount, IReadOnlyList<TEntity> Data)> ListAsync(Specification<TEntity> specification, CancellationToken cancellationToken = default);


    }

}
