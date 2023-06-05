using System.Text.Json.Serialization;

namespace MyEventSourcing.MS.Domain.Entities.Cart.Events;

public class ProductQuantityChangedEvent : MSDomainEvent
{
    [JsonIgnore]
    //не менять. для обратной совместимости с БД
    public const string EVENT_TYPE_CONSTANT = "ProductQuantityChangedEvent";

    public Guid CartItemId { get; private set; }
    public Guid ProductId { get; private set; }
    public int NewQuantity { get; private set; }

    internal ProductQuantityChangedEvent(Guid id, Guid cartItemId, Guid productId, int newQuantity,
        Guid aggregateId, long aggregateVersion)
        : base(id, aggregateId, aggregateVersion)
    {
        CartItemId = cartItemId;
        ProductId = productId;
        NewQuantity = newQuantity;
    }
}