using MediatR;
using MyEventSourcing.MS.Contract;
using MyEventSourcing.MS.Contract.DTO;
using MyEventSourcing.MS.Contract.Requests;
using MyEventSourcing.MS.Domain.Mappers;
using MyEventSourcing.MS.Domain.Services.Command;

namespace MyEventSourcing.MS.Domain.APIHandlers;

public class CartUpdateAddProductHandler : IRequestHandler<CartUpdateAddProductRequest, Response<CartDTO>>
{
    private readonly CartMapper _cartMapper;
    private readonly CartCommandService _cartCommandService;

    public CartUpdateAddProductHandler(CartMapper cartMapper, CartCommandService cartCommandService)
    {
        _cartMapper = cartMapper;
        _cartCommandService = cartCommandService;
    }

    public async Task<Response<CartDTO>> Handle(CartUpdateAddProductRequest request, CancellationToken cancellationToken)
    {
        var res = await _cartCommandService.UpdateAddProductAsync(request.CartItemId, request.ProductId, request.Quantity);
        var dto = _cartMapper.Map(res);
        return new Response<CartDTO>(dto);
    }
}