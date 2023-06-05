using MediatR;
using MyEventSourcing.MS.Contract;
using MyEventSourcing.MS.Contract.DTO;
using MyEventSourcing.MS.Contract.Requests;
using MyEventSourcing.MS.Domain.Mappers;
using MyEventSourcing.MS.Domain.Services.Query;

namespace MyEventSourcing.MS.Domain.APIHandlers;

public class CartReadHandler : IRequestHandler<CartReadRequest, Response<CartDTO>>
{
    private readonly CartMapper _cartMapper;
    private readonly CartQueryService _cartQueryService;

    public CartReadHandler(CartMapper cartMapper, CartQueryService cartQueryService)
    {
        _cartMapper = cartMapper;
        _cartQueryService = cartQueryService;
    }

    public async Task<Response<CartDTO>> Handle(CartReadRequest request, CancellationToken cancellationToken)
    {
        var res = await _cartQueryService.ReadAsync(request.Id, request.IncludeItems);
        var dto = _cartMapper.Map(res);
        return new Response<CartDTO>(dto);
    }
}