using System.Text.Json.Serialization;

namespace MyEventSourcing.MS.Domain.Entities.Cart.Events;

public class ProductAddedEvent : MSDomainEvent
{
    [JsonIgnore]
    //не менять. для обратной совместимости с БД
    public const string EVENT_TYPE_CONSTANT = "ProductAddedEvent";
    public Guid CartItemId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }

    internal ProductAddedEvent(Guid id, Guid cartItemId, Guid productId, int quantity,
        Guid aggregateId, long aggregateVersion)
        : base(id, aggregateId, aggregateVersion)
    {
        CartItemId = cartItemId;
        ProductId = productId;
        Quantity = quantity;
    }
}