using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyEventSourcing.MS.Contract.Requests;

namespace MyEventSourcing.MS.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly IMediator _mediator;

    public CartController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("read")]
    public async Task<IActionResult> ReadAsync(Guid id)
    {
        var request = new CartReadRequest(id, true);
        var res = await _mediator.Send(request);
        return Ok(res);
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateAsync(CartCreateRequest request)
    {
        var res = await _mediator.Send(request);
        return Ok(res);
    }

    [HttpPost]
    [Route("add-product")]
    public async Task<IActionResult> UpdateAsync(CartUpdateAddProductRequest request)
    {
        var res = await _mediator.Send(request);
        return Ok(res);
    }

    [HttpPost]
    [Route("change-product-count")]
    public async Task<IActionResult> UpdateAsync(CartUpdateChangeProductQuantityRequest request)
    {
        var res = await _mediator.Send(request);
        return Ok(res);
    }
}