using Asp.Versioning;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Pagination;
using CatalogService.UseCases.Book.Commands.AddBook;
using CatalogService.UseCases.Book.Commands.DeleteBook;
using CatalogService.UseCases.Book.Commands.UpdateBook;
using CatalogService.UseCases.Book.Queries.GetBookModelById;
using CatalogService.UseCases.Book.Queries.GetBooksByFilter;
using CatalogService.UseCases.Book.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CatalogService.Endpoints;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class BookEndpoints(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<BookViewModel>>> GetByFilter(
        [FromQuery] string? title,
        [FromQuery] string? author,
        [FromQuery] int pageSize = 20,
        [FromQuery] int pageNumber = 1,
        [FromQuery] OrderType orderType = OrderType.Ascending,
        CancellationToken cancellationToken = default)
    {
        var query = new GetBooksByFilterQuery(title, author, pageSize, pageNumber, orderType);
        var (isValid, errors) = query.Validate();
        if (!isValid)
        {
            return BadRequest(new { errors });
        }

        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<BookViewModel>> GetById(
        [Range(1, long.MaxValue)] long id,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetBookByIdQuery(id), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<long>> Add(
        [FromBody] AddBookCommand command,
        CancellationToken cancellationToken)
    {
        var (isValid, errors) = command.Validate();
        if (!isValid)
        {
            return BadRequest(new { errors });
        }

        var id = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(
        [Range(1, long.MaxValue)] long id,
        [FromBody] UpdateBookCommand request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateBookCommand(id, request.Title, request.Author, request.Stock, request.Price);
        var (isValid, errors) = command.Validate();
        if (!isValid)
        {
            return BadRequest(new { errors });
        }

        await mediator.Send(command, cancellationToken);
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
