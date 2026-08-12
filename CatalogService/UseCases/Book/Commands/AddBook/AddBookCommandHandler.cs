using BuildingBlocks.CQRS;
using CatalogService.Entities;
using Marten;

namespace CatalogService.UseCases.Book.Commands.AddBook;

public class AddBookCommandHandler(IDocumentSession session) : ICommandHandler<AddBookCommand, long>
{
    public async Task<long> Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        var book = new Entities.Book
        {
            Title = request.Title,
            Author = request.Author,
            Stock = request.Stock,
            Price = request.Price
        };

        session.Store(book);
        await session.SaveChangesAsync(cancellationToken);

        return book.Id;
    }
}
