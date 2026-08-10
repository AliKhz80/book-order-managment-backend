using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces.Repositories.BusinessIRepositories.OrderRepository;

namespace OrderService.Infrastructure.Repositories.BusinessRepositories.OrderRepository;

public class OrderRepositoryQuery(OrderDbContext dbContext) : RepositoryQuery<Order, long>(dbContext), IOrderRepositoryQuery
{
}
