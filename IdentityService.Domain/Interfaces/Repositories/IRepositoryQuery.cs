using IdentityService.Domain.Entities.BaseEntity;
using BuildingBlocks.Specification;
using System.Numerics;

namespace IdentityService.Domain.Interfaces.Repositories
{
    public interface IRepositoryQuery<TEntity,T> where TEntity : Entity<T> where T : INumber<T>
    {
        Task<TEntity?> GetByIdAsync(T id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<(int TotalCount, IReadOnlyList<TEntity> Data)> ListAsync(Specification<TEntity> specification, CancellationToken cancellationToken = default);


    }

}
