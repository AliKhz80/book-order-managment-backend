using System.ComponentModel.DataAnnotations;
using MediatR;

namespace CatalogService.Application.UseCases.Book.Commands.DeleteBook;

public record DeleteBookCommand(
    long Id
) : IRequest;
