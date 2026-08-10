using OrderService.Domain.Interfaces.Repositories.BusinessIRepositories.OrderRepository;

namespace OrderService.Domain.Interfaces;

public interface IOrderRepository : IOrderRepositoryCommond, IOrderRepositoryQuery
{
}
