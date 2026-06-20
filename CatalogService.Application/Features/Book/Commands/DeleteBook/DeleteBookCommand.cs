using System.ComponentModel.DataAnnotations;
using MediatR;

namespace CatalogService.Application.Features.Book.Commands.DeleteBook;

public record DeleteBookCommand(
    long Id
) : IRequest;
