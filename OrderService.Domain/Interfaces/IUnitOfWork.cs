using OrderService.Domain.Interfaces.Repositories.BusinessIRepositories.OrderRepository;

namespace OrderService.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    public IOrderRepositoryQuery OrderRepositoryQuery { get; }
    public IOrderRepositoryCommond OrderRepositoryCommond { get; }

    void Commit();
    public Task CommitAsync();
    void Rollback();
    public Task RollbackAsync();
    public Task BeginTransactionAsync();
}

public interface IOrderUnitOfWork : IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken = default);
}
