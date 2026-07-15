using IdentityService.Domain.Interfaces.Repositories.BusinessIRepositories.UserRepository;

namespace IdentityService.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
       public IUserRepositoryCommond UserRepository { get; }

        void Commit();

        public Task CommitAsync();

        void Rollback();

        public Task RollbackAsync();

        public Task BeginTransactionAsync();

    }
}
