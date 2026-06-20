using System.ComponentModel.DataAnnotations;
using MediatR;

namespace CatalogService.Application.Features.Book.Commands.UpdateBook;

public record UpdateBookCommand(
    long Id,

   
    string Title,

  
    string Author,

    
    long Stock,

    int Price
) : IRequest;
