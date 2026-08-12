using CatalogService.Domain.Interfaces;
using MediatR;

namespace CatalogService.Application.UseCases.Book.Commands.AddBook;

public class AddBookCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddBookCommand, long>
{
    public async Task<long> Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        var book = new Domain.Entities.Book
        {
            Id = default,
            Title = request.Title,
            Author = request.Author,
            Stock = request.Stock,
            Price = request.Price
        };

        await unitOfWork.BookRepositoryCommond.AddAsync(book, cancellationToken);
        await unitOfWork.CommitAsync();

        return book.Id;
    }
}
