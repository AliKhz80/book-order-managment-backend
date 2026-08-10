using Asp.Versioning;
using CatalogService.Application.Common.QueryModels;
using CatalogService.Application.UseCases.Book.Commands.DeleteBook;
using CatalogService.Application.UseCases.Book.Queries.GetBookModelById;
using CatalogService.Application.UseCases.Book.Queries.GetBooksByFilter;
using CatalogService.Application.UseCases.Book.Commands.AddBook;
using CatalogService.Application.UseCases.Book.Commands.UpdateBook;
using CatalogService.Application.UseCases.Book.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CatalogService.Presentation.Endpoints;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class BookEndpoints(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<Paging<BookViewModel>>> GetByFilter(
        [FromQuery] string? title,
        [FromQuery] string? author,
        [FromQuery] int pageSize = 20,
        [FromQuery] int pageNumber = 1,
        [FromQuery] OrderType orderType = OrderType.Ascending,
        CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(
            new GetBooksByFilterQuery(title, author, pageSize, pageNumber, orderType),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<BookViewModel>> GetById(
        [Range(1, long.MaxValue)] long id,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetBooklByIdQuery(id), cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<long>> Add(
        [FromBody] AddBookCommand command,
        CancellationToken cancellationToken)
    {
        var id = await mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(
        [Range(1, long.MaxValue)] long id,
        [FromBody] UpdateBookCommand request,
        CancellationToken cancellationToken)
    {
        await mediator.Send(
            new UpdateBookCommand(id, request.Title, request.Author, request.Stock, request.Price),
            cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(
        [Range(1, long.MaxValue)] long id,
        CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteBookCommand(id), cancellationToken);

        return NoContent();
    }
}
