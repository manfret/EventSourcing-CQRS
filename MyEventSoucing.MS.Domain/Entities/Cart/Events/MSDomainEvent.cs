using MyEventSourcing.Common.Domain.Core;
using PolyJson;

namespace MyEventSourcing.MS.Domain.Entities.Cart.Events;

[PolyJsonConverter(DISCRIMINATOR)]
[PolyJsonConverter.SubType(typeof(CartCreatedEvent), CartCreatedEvent.EVENT_TYPE_CONSTANT)]
[PolyJsonConverter.SubType(typeof(ProductAddedEvent), ProductAddedEvent.EVENT_TYPE_CONSTANT)]
[PolyJsonConverter.SubType(typeof(ProductQuantityChangedEvent), ProductQuantityChangedEvent.EVENT_TYPE_CONSTANT)]
public class MSDomainEvent : DomainEvent
{
    protected MSDomainEvent(Guid id, Guid aggregateId, long aggregateVersion)
        : base(id, aggregateId, aggregateVersion)
    { }
}