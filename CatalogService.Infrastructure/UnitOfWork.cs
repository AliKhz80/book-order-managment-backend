using CatalogService.Domain.Interfaces;
using CatalogService.Domain.Interfaces.Repositories.BusinessIRepositories.BookRepository;

namespace CatalogService.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CatalogDBContext _context;

        public UnitOfWork(
            CatalogDBContext context,
            IBookRepositoryQuery _bookRepository,
            IBookRepositoryCommond _bookRepositoryCommond)
        {
            _context = context;
            BookRepositoryCommond = _bookRepositoryCommond;
            BookRepositoryQuery = _bookRepository;

        }

        public IBookRepositoryQuery BookRepositoryQuery {  get; }

        public IBookRepositoryCommond BookRepositoryCommond {  get; }

        public void Commit() => _context.SaveChanges();

        public void Rollback() => _context.Database.RollbackTransaction();// Rollback changes if needed

        public async Task RollbackAsync() => await _context.Database.RollbackTransactionAsync();

        public async Task CommitAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();

        public async Task BeginTransactionAsync() => await _context.Database.BeginTransactionAsync();

    }
}
