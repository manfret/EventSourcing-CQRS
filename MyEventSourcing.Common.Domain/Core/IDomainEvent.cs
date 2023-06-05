using MyEventSourcing.Common.Domain.Data;
using PolyJson;
using System.Text.Json.Serialization;

namespace MyEventSourcing.Common.Domain.Core;

public abstract class DomainEvent : IEntity, IEquatable<DomainEvent>
{
    public const string DISCRIMINATOR = "EVENT_TYPE";
    public Guid Id { get; }
    public Guid AggregateId { get; }
    public long AggregateVersion { get; }

    [JsonPropertyName(DISCRIMINATOR)]
    public string Discriminator => DiscriminatorValue.Get(GetType())!;

    protected DomainEvent(
        Guid id, 
        Guid aggregateId, 
        long aggregateVersion)
    {
        Id = id;
        AggregateId = aggregateId;
        AggregateVersion = aggregateVersion;
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj);
    }

    public bool Equals(DomainEvent? other)
    {
        return other != null && Id.Equals(other.Id);
    }

    public override int GetHashCode()
    {
        return 290933282 + EqualityComparer<Guid>.Default.GetHashCode(Id);
    }
}

public class Event : IEntity
{
    public Guid Id { get; }
    public Guid AggregateId { get; }
    public long AggregateVersion { get; }
    public DomainEvent DomainEvent { get; }

    public Event(DomainEvent domainEvent)
    {
        Id = domainEvent.Id;
        AggregateId = domainEvent.AggregateId;
        AggregateVersion = domainEvent.AggregateVersion;
        DomainEvent = domainEvent;
    }
}