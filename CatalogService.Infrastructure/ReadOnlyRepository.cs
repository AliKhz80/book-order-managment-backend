using CatalogService.Domain.Interfaces;
using CatalogService.Domain.ModelConfigs;
using CatalogService.Domain.SpecificationConfig;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace CatalogService.Infrastructure;

public class ReadOnlyRepository<TEntity ,T>(
    DbContext dbContext
    ) : RepositoryProperties<TEntity ,T>(dbContext),IReadOnlyRepository<TEntity ,T> where TEntity : Entity<T> where T : INumber<T>
{

    public async Task<TEntity?> GetByIdAsync(T id, CancellationToken cancellationToken = default)
    {
        return await SetAsNoTracking.SingleOrDefaultAsync(x => Equals(x.Id, id), cancellationToken);
    }

    public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await SetAsNoTracking.ToListAsync(cancellationToken);
    }


    public async Task<(int TotalCount, IReadOnlyList<TEntity> Data)> ListAsync(Specification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        var query = SetAsNoTracking.Specify(specification);

        var totalCount = 0;

        if (specification.IsPagingEnabled)
        {
            totalCount = await query.CountAsync(cancellationToken);
            query = query.Skip(specification.Skip).Take(specification.Take);
        }
        var data = await query.ToListAsync(cancellationToken);

        return (totalCount, data);
    }

   
}
