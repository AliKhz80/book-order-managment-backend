using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.UseCases.Order.Commands.AddOrder;

namespace OrderService.Presentation.Endpoints;

[ApiController]
[Route("api/[controller]")]
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
