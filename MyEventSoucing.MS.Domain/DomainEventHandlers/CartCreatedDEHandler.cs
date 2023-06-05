using MyEventSourcing.Common.Domain.Data;
using MyEventSourcing.MS.Domain.Entities.Cart.Events;
using MyEventSourcing.MS.Domain.ReadModel.Cart;
using MyEventSourcing.MS.Domain.Repository;
using MyEventSourcing.MS.Domain.Services.Query;

namespace MyEventSourcing.MS.Domain.DomainEventHandlers;

public class CartCreatedDEHandler : DomainEventHandler<CartCreatedEvent>
{
    private readonly CustomerQueryService _customerQueryService;
    private readonly ICartRepository _cartRepository;

    public CartCreatedDEHandler(
        CustomerQueryService customerQueryService, 
        ICartRepository cartRepository)
    {
        _customerQueryService = customerQueryService;
        _cartRepository = cartRepository;
    }

    public override async Task HandleAsync(CartCreatedEvent @event)
    {
        var customer = (await _customerQueryService.ReadAsync(@event.CustomerId))!;
        var cartRM = new CartRM(
            @event.AggregateId,
            0,
            customer.Id,
            customer.Name);
        await _cartRepository.CreateAsync(cartRM);
    }
}