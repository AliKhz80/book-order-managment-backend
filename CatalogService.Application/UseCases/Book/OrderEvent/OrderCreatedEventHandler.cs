using CatalogService.Application.Messaging;
using CatalogService.Application.UseCases.Book.OrderEvent.EventModels;
using CatalogService.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace CatalogService.Application.UseCases.Book.OrderEvent;

public class OrderCreatedEventHandler(
    IUnitOfWork unitOfWork,
    IEventBus eventBus,
    IDistributedCache cache) : IIntegrationEventHandler<OrderCreatedEvent>
{
    public async Task HandleAsync(OrderCreatedEvent @event, CancellationToken cancellationToken = default)
    {
        var book = await unitOfWork.BookRepositoryQuery.GetByIdAsync(@event.BookId, cancellationToken);

        if (book is null)
        {
            await PublishStockFailedAsync(@event, "Book was not found.", cancellationToken);
            return;
        }

        if (book.Stock < @event.Quantity)
        {
            await PublishStockFailedAsync(@event, "Book stock is insufficient.", cancellationToken);
            return;
        }

        book.Stock -= @event.Quantity;

        await unitOfWork.BookRepositoryCommond.UpdateAsync(book, cancellationToken);
        await unitOfWork.CommitAsync();
        await cache.RemoveAsync(BookCacheKeys.ById(book.Id), cancellationToken);

        await eventBus.PublishAsync(
            new StockReservedEvent(@event.OrderId, @event.BookId, @event.Quantity),
            "stock.reserved",
            cancellationToken);
    }

    private async Task PublishStockFailedAsync(OrderCreatedEvent @event, string reason, CancellationToken cancellationToken)
    {
        await eventBus.PublishAsync(
            new StockFailedEvent(@event.OrderId, @event.BookId, @event.Quantity, reason),
            "stock.failed",
            cancellationToken);
    }
}
