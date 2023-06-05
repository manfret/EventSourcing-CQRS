using Microsoft.EntityFrameworkCore;
using MyEventSourcing.Common.Domain.Data;
using MyEventSourcing.MS.Domain.Entities.Cart.Events;
using MyEventSourcing.MS.Domain.Repository;

namespace MyEventSourcing.MS.Domain.DomainEventHandlers;

public class CartProductChangeQuantityHandler : DomainEventHandler<ProductQuantityChangedEvent>
{
    private readonly ICartItemRepository _cartItemRepository;
    private readonly ICartRepository _cartRepository;

    public CartProductChangeQuantityHandler(
        ICartItemRepository cartItemRepository, 
        ICartRepository cartRepository)
    {
        _cartItemRepository = cartItemRepository;
        _cartRepository = cartRepository;
    }

    public override async Task HandleAsync(ProductQuantityChangedEvent @event)
    {
        var cart = await _cartRepository.ReadAsync(@event.AggregateId);
        var cartItem = await _cartItemRepository.ReadAsync(rm => rm.CartId == @event.AggregateId &&
                                                                 rm.ProductId == @event.ProductId).SingleAsync();
        cart!.TotalItems += @event.NewQuantity - cartItem.Quantity;
        cartItem.Quantity = @event.NewQuantity;

        _cartRepository.Update(cart);
        await _cartItemRepository.UpdateAsync(cartItem);
    }
}