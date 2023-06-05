using MyEventSourcing.Common.Domain.Core;

namespace MyEventSourcing.Common.Domain.Data;

public interface IDomainEventHandler
{
    Task HandleAsync(object @event);
}

public abstract class DomainEventHandler<TEvent> : IDomainEventHandler
    where TEvent: DomainEvent
{
    public async Task HandleAsync(object @event)
    {
        await HandleAsync((TEvent)@event);
    }
    public abstract Task HandleAsync(TEvent @event);
}