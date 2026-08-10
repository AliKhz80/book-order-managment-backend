using OrderService.Application.Common.CurrentUser;
using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces;

namespace OrderService.Infrastructure.Repositories.BusinessRepositories.OrderRepository;

public class OrderRepository(OrderDbContext dbContext, ICurrentUser currentUser) : RepositoryCommond<Order, long>(dbContext, currentUser), IOrderRepository
{
}
