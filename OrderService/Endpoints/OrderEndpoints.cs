using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.UseCases.Order.Commands.AddOrder;

namespace OrderService.Presentation.Endpoints;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class OrderEndpoints(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<long>> Add(
        [FromBody] AddOrderCommand command,
        CancellationToken cancellationToken)
    {
        var id = await mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(Add), new { id }, id);
    }
}
