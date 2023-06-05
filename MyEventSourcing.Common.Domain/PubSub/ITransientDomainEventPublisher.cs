namespace MyEventSourcing.Common.Domain.PubSub;

public interface ITransientDomainEventPublisher
{
    Task PublishAsync<T>(T publishedEvent);
}