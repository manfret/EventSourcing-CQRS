using MyEventSourcing.Common.Domain.Core;
using MyEventSourcing.Common.Domain.Data;
using MyEventSourcing.MS.Domain.Entities.Cart.Events;

namespace MyEventSourcing.MS.Domain.Entities.Cart;

public class Cart : AggregateBase
{
    private const int PRODUCT_QUANTITY_THRESHOLD = 50;
    public Guid CustomerId { get; private set; }
    public List<CartItem> Items { get; private set; }

    //нужен для развертывания аггрегата
    private Cart()
    {
        Items = new List<CartItem>();
    }

    //создание не из развертки - настоящее создание
    public Cart(Guid cartId, Guid customerId, DomainEventHandler<CartCreatedEvent> handler) : this()
    {
        var newEventId = Guid.NewGuid();
        RaiseEvent(new CartCreatedEvent(newEventId, cartId, customerId), handler);
    }

    public void AddProduct(Guid productId, int quantity, DomainEventHandler<ProductAddedEvent> handler)
    {
        if (ContainsProduct(productId))
        {
            throw new CartException($"Product {productId} already added");
        }
        CheckQuantity(productId, quantity);
        var newEventId = Guid.NewGuid();
        var cartItemId = Guid.NewGuid();
        RaiseEvent(new ProductAddedEvent(newEventId, cartItemId, productId, quantity, Id, Version), handler);
    }

    public void ChangeProductQuantity(Guid productId, int quantity, DomainEventHandler<ProductQuantityChangedEvent> handler)
    {
        if (!ContainsProduct(productId))
        {
            throw new CartException($"Product {productId} not found");
        }
        CheckQuantity(productId, quantity);
        var newEventId = Guid.NewGuid();
        var cartItem = GetCartItemByProduct(productId);
        RaiseEvent(new ProductQuantityChangedEvent(newEventId, cartItem.CartItemId, cartItem.ProductId, quantity, Id, Version), handler);
    }

    internal void Apply(CartCreatedEvent ev)
    {
        Id = ev.AggregateId;
        CustomerId = ev.CustomerId;
    }

    internal void Apply(ProductAddedEvent ev)
    {
        Items.Add(new CartItem(ev.CartItemId, ev.ProductId, ev.Quantity));
    }

    internal void Apply(ProductQuantityChangedEvent ev)
    {
        var existingItem = Items.Single(x => x.CartItemId == ev.CartItemId);

        Items.Remove(existingItem);
        var cartItem = new CartItem(ev.CartItemId, ev.ProductId, ev.NewQuantity);
        Items.Add(cartItem);
    }

    private bool ContainsProduct(Guid productId)
    {
        return Items.Any(x => x.ProductId == productId);
    }

    private CartItem GetCartItemByProduct(Guid productId)
    {
        return Items.Single(x => x.ProductId == productId);
    }

    private static void CheckQuantity(Guid productId, int quantity)
    {
        switch (quantity)
        {
            case <= 0:
                throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));
            case > PRODUCT_QUANTITY_THRESHOLD:
                throw new CartException($"Quantity for product {productId} must be less than or equal to {PRODUCT_QUANTITY_THRESHOLD}");
        }
    }

    public override string ToString()
    {
        return $"{{ CartItemId: \"{Id}\", CustomerId: \"{CustomerId}\", Items: [{string.Join(", ", Items.Select(x => x.ToString()))}] }}";
    }
}