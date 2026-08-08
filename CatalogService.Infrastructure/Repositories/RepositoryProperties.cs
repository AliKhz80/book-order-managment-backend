using CatalogService.Domain.Entities.BaseEntity;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace CatalogService.Infrastructure.Repositories;

public class RepositoryProperties<TEntity , T>(
    DbContext dbContext
    ) where TEntity : Entity<T> where T : INumber<T>
{
    protected readonly DbContext _dbContext = dbContext;

    protected DbSet<TEntity> Set => _dbContext.Set<TEntity>();

    protected IQueryable<TEntity> SetAsNoTracking
    {
        get
        {
            var query = Set.AsNoTracking();

            if (typeof(TEntity).IsSubclassOf(typeof(TrackableEntity<T>)))
            {
                query = query.Where(e => !(e as TrackableEntity<T>)!.IsDeleted);
            }

            return query;
        }
    }
}
