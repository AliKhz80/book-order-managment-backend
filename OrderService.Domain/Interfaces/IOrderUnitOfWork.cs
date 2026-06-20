namespace OrderService.Domain.Interfaces;

public interface IOrderUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken = default);
}
