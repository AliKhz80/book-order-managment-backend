using IdentityService.Domain.Interfaces.BusinessIRepositories;

namespace IdentityService.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
       public IUserRepository UserRepository { get; }

        void Commit();

        public Task CommitAsync();

        void Rollback();

        public Task RollbackAsync();

        public Task BeginTransactionAsync();

    }
}
