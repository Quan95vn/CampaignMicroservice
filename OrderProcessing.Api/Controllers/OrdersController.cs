using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderProcessing.Application.Command;

namespace OrderProcessing.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<OrderResponse>> CreateOrder(CreateOrderCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Unable to create order");
            }
        }
        catch (System.Exception ex)
        {
            // Log the exception
            return StatusCode(500, "An error occurred while processing your request");
        }
    }
}