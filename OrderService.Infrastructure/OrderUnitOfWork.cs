using OrderService.Domain.Interfaces;

namespace OrderService.Infrastructure;

public class OrderUnitOfWork(OrderDbContext dbContext) : IOrderUnitOfWork
{
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
