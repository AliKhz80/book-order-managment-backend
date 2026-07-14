using IdentityService.Domain.Interfaces;
using IdentityService.Domain.Interfaces.BusinessIRepositories;

namespace IdentityService.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IdentityDbContext _context;

        public UnitOfWork(
            IdentityDbContext context,
            IUserRepository _userRepository)
        {
            _context = context;
            UserRepository = _userRepository;

        }


        public IUserRepository UserRepository{ get; }

        public void Commit() => _context.SaveChanges();

        public void Rollback() => _context.Database.RollbackTransaction();// Rollback changes if needed

        public async Task RollbackAsync() => await _context.Database.RollbackTransactionAsync();

        public async Task CommitAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();

        public async Task BeginTransactionAsync() => await _context.Database.BeginTransactionAsync();

    }
}
