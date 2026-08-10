using OrderService.Application.Common.CurrentUser;
using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces.Repositories.BusinessIRepositories.OrderRepository;

namespace OrderService.Infrastructure.Repositories.BusinessRepositories.OrderRepository;

public class OrderRepositoryCommond(OrderDbContext dbContext, ICurrentUser currentUser) : RepositoryCommond<Order, long>(dbContext, currentUser), IOrderRepositoryCommond
{
}
