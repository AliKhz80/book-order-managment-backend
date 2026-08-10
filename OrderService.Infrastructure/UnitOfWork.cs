using OrderService.Domain.Interfaces;
using OrderService.Domain.Interfaces.Repositories.BusinessIRepositories.OrderRepository;

namespace OrderService.Infrastructure;

public class UnitOfWork : IUnitOfWork, IOrderUnitOfWork
{
    private readonly OrderDbContext _context;

    public UnitOfWork(
        OrderDbContext context,
        IOrderRepositoryQuery orderRepositoryQuery,
        IOrderRepositoryCommond orderRepositoryCommond)
    {
        _context = context;
        OrderRepositoryQuery = orderRepositoryQuery;
        OrderRepositoryCommond = orderRepositoryCommond;
    }

    public IOrderRepositoryQuery OrderRepositoryQuery { get; }
    public IOrderRepositoryCommond OrderRepositoryCommond { get; }

    public void Commit() => _context.SaveChanges();

    public void Rollback() => _context.Database.RollbackTransaction();

    public async Task RollbackAsync() => await _context.Database.RollbackTransactionAsync();

    public async Task CommitAsync() => await _context.SaveChangesAsync();

    public async Task CommitAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);

    public void Dispose() => _context.Dispose();

    public async Task BeginTransactionAsync() => await _context.Database.BeginTransactionAsync();
}
