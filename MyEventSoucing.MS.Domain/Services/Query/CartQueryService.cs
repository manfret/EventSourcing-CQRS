using MyEventSourcing.Common.Domain.Data;
using MyEventSourcing.MS.Domain.Entities.Cart;
using MyEventSourcing.MS.Domain.ReadModel.Cart;
using MyEventSourcing.MS.Domain.Repository;

namespace MyEventSourcing.MS.Domain.Services.Query;

public class CartQueryService
{
    private readonly ICartRepository _cartRepository;
    private readonly EventSourcingRepository<Cart> _eventSourcingRepository;
    private readonly CustomerQueryService _customerQueryService;

    public CartQueryService(
        ICartRepository cartRepository,
        EventSourcingRepository<Cart> eventSourcingRepository,
        CustomerQueryService customerQueryService)
    {
        _cartRepository = cartRepository;
        _eventSourcingRepository = eventSourcingRepository;
        _customerQueryService = customerQueryService;
    }

    public async Task<CartRM> ReadAsync(Guid id, bool includeItems = false)
    {
        var res = await _cartRepository.ReadAsync(id, true);
        if (res != null) return res;

        //читаем из стора и сразу сворачиваем
        var aggregate = await _eventSourcingRepository.ReadByAggregateIdAsync(id);

        //записываем в репозиторий
        var customer = (await _customerQueryService.ReadAsync(aggregate.CustomerId))!;
        var cartRM = new CartRM(
            aggregate.Id,
            aggregate.Items.Count,
            customer.Id,
            customer.Name);

        //записываем в репозиторий в фоне
        _ = _cartRepository.CreateAsync(cartRM);
        return cartRM;
    }
}