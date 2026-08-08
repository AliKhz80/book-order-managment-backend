using CatalogService.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace CatalogService.Application.UseCases.Book.Commands.AddBook;

public class AddBookCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddBookCommand, long>
{
    public async Task<long> Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        var book = new Domain.Models.Book
        {
            Id = default,
            Title = request.Title,
            Author = request.Author,
            Stock = request.Stock,
            Price = request.Price
        };

        await unitOfWork.BookRepository.AddAsync(book, cancellationToken);
        await unitOfWork.CommitAsync();

        return book.Id;
    }
}
