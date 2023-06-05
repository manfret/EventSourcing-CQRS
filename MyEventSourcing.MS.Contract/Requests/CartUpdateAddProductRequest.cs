using MediatR;
using MyEventSourcing.MS.Contract.DTO;

namespace MyEventSourcing.MS.Contract.Requests;

public class CartUpdateAddProductRequest : IRequest<Response<CartDTO>>
{
    public Guid CartItemId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }

    public CartUpdateAddProductRequest(
        Guid cartItemId, 
        Guid productId, 
        int quantity)
    {
        CartItemId = cartItemId;
        ProductId = productId;
        Quantity = quantity;
    }
}