using System.Text.Json.Serialization;
using MyEventSourcing.Common.Domain.Core;

namespace MyEventSourcing.MS.Domain.Entities.Cart.Events;

public class CartCreatedEvent : MSDomainEvent
{
    [JsonIgnore]
    //не менять. для обратной совместимости с БД
    public const string EVENT_TYPE_CONSTANT = "CartCreatedEvent";

    public Guid CustomerId { get; private set; }

    internal CartCreatedEvent(Guid id, Guid aggregateId, Guid customerId)
        : base(id, aggregateId, AggregateBase.VERSION_NEW)
    {
        CustomerId = customerId;
    }
}