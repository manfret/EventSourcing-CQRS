using MyEventSourcing.Common.Domain.Data;
using MyEventSourcing.MS.Domain.Entities.Cart.Events;
using MyEventSourcing.MS.Domain.ReadModel.Cart;
using MyEventSourcing.MS.Domain.Repository;
using MyEventSourcing.MS.Domain.Services.Query;

namespace MyEventSourcing.MS.Domain.DomainEventHandlers;

public class CartProductAddedDEHandler : DomainEventHandler<ProductAddedEvent>
{
    private readonly ProductQueryService _productQueryService;
    private readonly ICartRepository _cartRepository;
    private readonly ICartItemRepository _cartItemRepository;

    public CartProductAddedDEHandler(
        ProductQueryService productQueryService, 
        ICartRepository cartRepository, 
        ICartItemRepository cartItemRepository)
    {
        _productQueryService = productQueryService;
        _cartRepository = cartRepository;
        _cartItemRepository = cartItemRepository;
    }

    public override async Task HandleAsync(ProductAddedEvent @event)
    {
        var product = await _productQueryService.ReadAsync(@event.ProductId);
        var cart = await _cartRepository.ReadAsync(@event.AggregateId);
        var cartItem = new CartItemRM(
            Guid.NewGuid(),
            cart!.Id,
            product!.Id,
            product.Name,
            @event.Quantity);
        cart.TotalItems += @event.Quantity;

        _cartRepository.Update(cart);
        await _cartItemRepository.CreateAsync(cartItem);
    }
}