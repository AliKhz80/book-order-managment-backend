using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using CatalogService.Entities;
using CatalogService.UseCases.Book;
using Marten;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace CatalogService.UseCases.Book.Commands.UpdateBook;

public class UpdateBookCommandHandler(
    IDocumentSession session,
    IDistributedCache cache) : ICommandHandler<UpdateBookCommand>
{
    public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await session.LoadAsync<Entities.Book>(request.Id, cancellationToken)
            ?? throw new NotFoundException("Book", request.Id);

        book.Title = request.Title;
        book.Author = request.Author;
        book.Stock = request.Stock;
        book.Price = request.Price;
        book.UpdatedAt = DateTime.UtcNow;

        session.Update(book);
        await session.SaveChangesAsync(cancellationToken);

        await cache.RemoveAsync(BookCacheKeys.ById(request.Id), cancellationToken);

        return Unit.Value;
    }
}
