using MediatR;
using MyEventSourcing.MS.Contract;
using MyEventSourcing.MS.Contract.DTO;
using MyEventSourcing.MS.Contract.Requests;
using MyEventSourcing.MS.Domain.Mappers;
using MyEventSourcing.MS.Domain.Services.Command;

namespace MyEventSourcing.MS.Domain.APIHandlers;

public class CreateCartHandler : IRequestHandler<CartCreateRequest, Response<CartDTO>>
{
    private readonly CartMapper _cartMapper;
    private readonly CartCommandService _cartCommandService;

    public CreateCartHandler(CartMapper cartMapper, CartCommandService cartCommandService)
    {
        _cartMapper = cartMapper;
        _cartCommandService = cartCommandService;
    }

    public async Task<Response<CartDTO>> Handle(CartCreateRequest request, CancellationToken cancellationToken)
    {
        var res = await _cartCommandService.CreateAsync(request.CustomerId);
        var dto = _cartMapper.Map(res);
        return new Response<CartDTO>(dto);
    }
}