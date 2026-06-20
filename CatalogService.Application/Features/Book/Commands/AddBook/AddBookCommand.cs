using System.ComponentModel.DataAnnotations;
using MediatR;

namespace CatalogService.Application.Features.Book.Commands.AddBook;

public record AddBookCommand(
    string Title,

    string Author,

    long Stock,

    int Price
) : IRequest<long>;
