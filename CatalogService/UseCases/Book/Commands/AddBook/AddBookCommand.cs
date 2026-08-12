using BuildingBlocks.CQRS;

namespace CatalogService.UseCases.Book.Commands.AddBook;

public record AddBookCommand(
    string Title,
    string Author,
    long Stock,
    int Price
) : ICommand<long>;
