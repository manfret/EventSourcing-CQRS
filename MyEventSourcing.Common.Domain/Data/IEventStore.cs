using MyEventSourcing.Common.Domain.Core;

namespace MyEventSourcing.Common.Domain.Data;

public interface IEventStore
{
    Task<IEnumerable<DomainEvent>> ReadForAggregateAsync(Guid aggregateId);
    Task AppendAsync(DomainEvent @event);
}