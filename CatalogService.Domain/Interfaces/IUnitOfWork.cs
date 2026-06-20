using CatalogService.Domain.Interfaces.BusinessIRepositories;

namespace CatalogService.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
       public IBookRepository BookRepository { get; }

        void Commit();

        public Task CommitAsync();

        void Rollback();

        public Task RollbackAsync();

        public Task BeginTransactionAsync();

    }
}
