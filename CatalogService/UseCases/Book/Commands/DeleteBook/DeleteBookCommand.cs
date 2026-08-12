using BuildingBlocks.CQRS;

namespace CatalogService.UseCases.Book.Commands.DeleteBook;

public record DeleteBookCommand(
    long Id
) : ICommand;
