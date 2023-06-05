using MyEventSourcing.Common.Domain.Data;

namespace MyEventSourcing.Common.Domain.Core;

public abstract class AggregateBase : IAggregate, IEventSourcingAggregate
{
    private readonly ICollection<DomainEvent> _uncommittedEvents = new LinkedList<DomainEvent>();
    private readonly Dictionary<DomainEvent, IDomainEventHandler> _handlers = new();

    public Guid Id { get; protected set; }

    protected long Version { get; private set; }
    public const long VERSION_NEW = 0;

    public void ApplyEvent(DomainEvent @event)
    {
        //если уже такой ивент есть в списке - ничего не делаем
        if (_uncommittedEvents.Any(x => Equals(x.Id, @event.Id))) return;

        ((dynamic)this).Apply((dynamic)@event);

        Version = @event.AggregateVersion;
        Version++;
    }

    void IEventSourcingAggregate.ClearUncommittedEvents()
    {
        _uncommittedEvents.Clear();
        _handlers.Clear();
    }

    IEnumerable<(DomainEvent @event, IDomainEventHandler handler)> IEventSourcingAggregate.GetUncommittedEvents()
    {
        return _handlers.Select(a => (a.Key, a.Value));
    }

    protected void RaiseEvent<TEvent>(TEvent @event, DomainEventHandler<TEvent> handler)
        where TEvent : DomainEvent
    {
        ApplyEvent(@event);
        _uncommittedEvents.Add(@event);
        _handlers.Add(@event, handler);
    }
}