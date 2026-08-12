using BuildingBlocks.CQRS;

namespace CatalogService.UseCases.Book.Commands.UpdateBook;

public record UpdateBookCommand(
    long Id,
    string Title,
    string Author,
    long Stock,
    int Price
) : ICommand;
