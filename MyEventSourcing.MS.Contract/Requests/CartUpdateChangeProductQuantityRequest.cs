using MediatR;
using MyEventSourcing.MS.Contract.DTO;

namespace MyEventSourcing.MS.Contract.Requests;

public class CartUpdateChangeProductQuantityRequest : IRequest<Response<CartDTO>>
{
    public Guid CartItemId { get; private set; }
    public Guid ProductId { get; private set; }
    //TODO: нужен oldQuantity ?
    public int NewQuantity { get; private set; }

    public CartUpdateChangeProductQuantityRequest(
        Guid cartItemId,
        Guid productId,
        int newQuantity)
    {
        CartItemId = cartItemId;
        ProductId = productId;
        NewQuantity = newQuantity;
    }
}