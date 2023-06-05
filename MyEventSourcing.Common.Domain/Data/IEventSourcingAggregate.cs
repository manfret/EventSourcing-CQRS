using MyEventSourcing.Common.Domain.Core;

namespace MyEventSourcing.Common.Domain.Data;

public interface IEventSourcingAggregate
{
    void ApplyEvent(DomainEvent @event);
    IEnumerable<(DomainEvent @event, IDomainEventHandler handler)> GetUncommittedEvents();
    void ClearUncommittedEvents();
}