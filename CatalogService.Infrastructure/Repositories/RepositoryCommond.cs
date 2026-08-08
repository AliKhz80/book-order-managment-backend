using CatalogService.Application.Common.CurrentUser;
using CatalogService.Domain.Entities.BaseEntity;
using CatalogService.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace CatalogService.Infrastructure.Repositories
{
    public class RepositoryCommond<TEntity,T>(DbContext _Dbcontext , ICurrentUser currentUser) : RepositoryQuery<TEntity,T>(_Dbcontext),
    IRepositoryCommond<TEntity , T> where TEntity : Entity<T> where T : INumber<T>
    {



        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity is TrackableEntity<T> trackable)
            {
                trackable.Created(currentUser.UserName);
            }

            await Set.AddAsync(entity, cancellationToken);
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity is TrackableEntity<T> trackable)
            {
                trackable.Updated(currentUser.UserName);
            }

            await Task.Run(() =>
            {
                Set.Update(entity);
            }, cancellationToken);
        }

       

        public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity is TrackableEntity<T> trackable)
            {
                trackable.Deleted(currentUser.UserName);

                await Task.Run(() =>
                {
                    Set.Update(entity);
                }, cancellationToken);
            }
            else
            {
                await Task.Run(() =>
                {
                    Set.Remove(entity);
                }, cancellationToken);
            }
        }

    }
}