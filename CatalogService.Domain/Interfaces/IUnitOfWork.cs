using CatalogService.Domain.Interfaces.Repositories.BusinessIRepositories.BookRepository;

namespace CatalogService.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
       public IBookRepositoryQuery BookRepositoryQuery { get; }
       public IBookRepositoryCommond BookRepositoryCommond { get; }

        void Commit();

        public Task CommitAsync();

        void Rollback();

        public Task RollbackAsync();

        public Task BeginTransactionAsync();

    }
}
