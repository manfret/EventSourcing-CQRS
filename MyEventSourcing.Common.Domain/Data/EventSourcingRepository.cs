using MyEventSourcing.Common.Domain.Core;
using System.Reflection;
using MyEventSourcing.Common.Domain.PubSub;

namespace MyEventSourcing.Common.Domain.Data;

public class EventSourcingRepository<TAggregate>
    where TAggregate : AggregateBase
{
    private readonly IEventStore _eventStore;
    private readonly ITransientDomainEventPublisher _publisher;

    public EventSourcingRepository(IEventStore eventStore, ITransientDomainEventPublisher publisher)
    {
        _eventStore = eventStore;
        _publisher = publisher;
    }

    public async Task<TAggregate> ReadByAggregateIdAsync(Guid aggregateId)
    {
        try
        {
            //создаем пустой объект
            var aggregate = CreateEmptyAggregate();
            //приводим к типу агрегата для ивент сорсинга
            IEventSourcingAggregate aggregatePersistence = aggregate;

            //достаем все события для такого агрегата
            foreach (var @event in await _eventStore.ReadForAggregateAsync(aggregateId))
            {
                //применяем ивент DomainEvent с версией EventNumber
                aggregatePersistence.ApplyEvent(@event);
            }
            return aggregate;
        }
        catch (EventStoreAggregateNotFoundException)
        {
            return null;
        }
        catch (EventStoreCommunicationException ex)
        {
            throw new RepositoryException("Unable to access persistence layer", ex);
        }
    }

    public async Task SaveAsync(TAggregate aggregate)
    {
        try
        {
            IEventSourcingAggregate aggregatePersistence = aggregate;

            //находим все незакомиченые события
            foreach (var (@event, handler) in aggregatePersistence.GetUncommittedEvents())
            {
                //записываем событие в БД
                //TODO: публикация может быть асинхронная на основе журнала или доп.таблицы OUTBOX
                await _eventStore.AppendAsync(@event);
                //публикуем событие в очереди
                await _publisher.PublishAsync((dynamic)@event);
                //находим обработчик
                //TODO: подписка на обработку может быть при успешной регистрации в очереди
                await handler.HandleAsync(@event);
            }
            //убираем все незакомиченые измнения
            aggregatePersistence.ClearUncommittedEvents();
        }
        catch (EventStoreCommunicationException ex)
        {
            throw new RepositoryException("Unable to access persistence layer", ex);
        }
    }

    private TAggregate CreateEmptyAggregate()
    {
        return (TAggregate)typeof(TAggregate)
            .GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,
                null, Type.EmptyTypes, Array.Empty<ParameterModifier>())!
                .Invoke(Array.Empty<object>());
    }
}


[Serializable]
public class RepositoryException : Exception
{
    public RepositoryException() { }
    public RepositoryException(string message) : base(message) { }
    public RepositoryException(string message, Exception inner) : base(message, inner) { }
    protected RepositoryException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}