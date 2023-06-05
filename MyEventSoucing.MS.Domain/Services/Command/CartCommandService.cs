using MyEventSourcing.Common.Domain.Data;
using MyEventSourcing.MS.Domain.DomainEventHandlers;
using MyEventSourcing.MS.Domain.Entities.Cart;
using MyEventSourcing.MS.Domain.ReadModel.Cart;
using MyEventSourcing.MS.Domain.Repository;
using MyEventSourcing.MS.Domain.Services.Query;

namespace MyEventSourcing.MS.Domain.Services.Command;

public class CartCommandService
{
    private readonly ICartRepository _cartRepository;
    private readonly CartQueryService _cartQueryService;
    private readonly EventSourcingRepository<Cart> _eventSourcingCartRepository;
    private readonly CustomerQueryService _customerQueryService;
    private readonly ProductQueryService _productQueryService;
    private readonly ICartItemRepository _cartItemRepository;

    public CartCommandService(
        ICartRepository cartRepository,
        CartQueryService cartQueryService,
        EventSourcingRepository<Cart> eventSourcingCartRepository,
        CustomerQueryService customerQueryService,
        ProductQueryService productQueryService,
        ICartItemRepository cartItemRepository)
    {
        _cartRepository = cartRepository;
        _cartQueryService = cartQueryService;
        _eventSourcingCartRepository = eventSourcingCartRepository;
        _customerQueryService = customerQueryService;
        _productQueryService = productQueryService;
        _cartItemRepository = cartItemRepository;
    }

    //каким образом получается много uncommitted? - если произвести несколько действий подряд (в текущем случае никак)
    //посмотреть - где мало надежности 
    //ОК - запросов не слишком много - запросы на чтение идут к быстрой БД - только командные запросы идут к медленной

    public async Task<CartRM> CreateAsync(Guid customerId)
    {
        //создаем обработчик события - создавать можно через рефлексию
        var eventHandler = new CartCreatedDEHandler(_customerQueryService, _cartRepository);

        //создаем аггрегат
        var cart = new Cart(Guid.NewGuid(), customerId, eventHandler);

        //сохраняем событие ES + публикация
        await _eventSourcingCartRepository.SaveAsync(cart);

        //находим текущее состояние RM
        return await _cartQueryService.ReadAsync(cart.Id);
    }

    public async Task<CartRM> UpdateAddProductAsync(Guid cartId, Guid productId, int quantity)
    {
        //создаем обработчик события - создавать можно через рефлексию
        var eventHandler = new CartProductAddedDEHandler(_productQueryService, _cartRepository, _cartItemRepository);

        //восстанавливаем аггрегат -> производим действие
        var cart = await _eventSourcingCartRepository.ReadByAggregateIdAsync(cartId);
        cart.AddProduct(productId, quantity, eventHandler);

        //сохраняем событие ES + публикация
        await _eventSourcingCartRepository.SaveAsync(cart);

        //находим текущее состояние RM
        return await _cartQueryService.ReadAsync(cart.Id);
    }

    public async Task<CartRM> UpdateProductQuntityAsync(Guid cartId, Guid productId, int newQuantity)
    {
        //создаем обработчик события - создавать можно через рефлексию
        var eventHandler = new CartProductChangeQuantityHandler(_cartItemRepository, _cartRepository);

        //восстанавливаем аггрегат -> производим действие
        var cart = await _eventSourcingCartRepository.ReadByAggregateIdAsync(cartId);
        cart.ChangeProductQuantity(productId, newQuantity, eventHandler);

        //сохраняем событие ES + публикация
        await _eventSourcingCartRepository.SaveAsync(cart);

        //находим текущее состояние RM
        return await _cartQueryService.ReadAsync(cart.Id);
    }
}