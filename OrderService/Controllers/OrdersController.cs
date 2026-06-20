using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Features.Orders.Commands.AddOrder;

namespace OrderService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(IMediator mediator) : ControllerBase
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
