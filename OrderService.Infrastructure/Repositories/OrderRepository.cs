using OrderService.Domain.Interfaces;
using OrderService.Domain.Models;

namespace OrderService.Infrastructure.Repositories;

public class OrderRepository(OrderDbContext dbContext) : IOrderRepository
{
    public async Task AddAsync(Order order, CancellationToken cancellationToken = default)
    {
        await dbContext.Orders.AddAsync(order, cancellationToken);
    }

    public async Task<Order?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Orders.FindAsync([id], cancellationToken);
    }

    public async Task UpdateAsync(Order order, CancellationToken cancellationToken = default)
    {
        await Task.Run(() => dbContext.Orders.Update(order), cancellationToken);
    }
}
