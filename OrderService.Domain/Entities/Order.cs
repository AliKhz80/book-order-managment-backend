using OrderService.Domain.Entities.BaseEntity;
using OrderService.Domain.Enums;

namespace OrderService.Domain.Entities;

public class Order : TrackableEntity<long>
{
    public long BookId { get; set; }
    public long Quantity { get; set; }
    public OrderStatus Status { get; set; }
}
