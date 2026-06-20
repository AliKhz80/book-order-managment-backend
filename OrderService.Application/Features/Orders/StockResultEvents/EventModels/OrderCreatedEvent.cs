namespace OrderService.Application.Features.Orders.StockResultEvents.EventModels
{
    public record OrderCreatedEvent(long OrderId, long BookId, long Quantity);
}
