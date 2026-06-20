using CatalogService.Domain.Interfaces;
using CatalogService.Domain.Interfaces.BusinessIRepositories;

namespace CatalogService.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CatalogDBContext _context;

        public UnitOfWork(
            CatalogDBContext context,
            IBookRepository _bookRepository)
        {
            _context = context;
            BookRepository = _bookRepository;

        }


        public IBookRepository BookRepository{ get; }

        public void Commit() => _context.SaveChanges();

        public void Rollback() => _context.Database.RollbackTransaction();// Rollback changes if needed

        public async Task RollbackAsync() => await _context.Database.RollbackTransactionAsync();

        public async Task CommitAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();

        public async Task BeginTransactionAsync() => await _context.Database.BeginTransactionAsync();

    }
}
