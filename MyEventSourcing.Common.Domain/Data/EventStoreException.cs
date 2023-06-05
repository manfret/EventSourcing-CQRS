namespace MyEventSourcing.Common.Domain.Data;

[Serializable]
public class EventStoreException : Exception
{
    public EventStoreException() { }
    public EventStoreException(string message) : base(message) { }
    public EventStoreException(string message, Exception inner) : base(message, inner) { }
}


[Serializable]
public class EventStoreAggregateNotFoundException : EventStoreException
{
    public EventStoreAggregateNotFoundException() { }
    public EventStoreAggregateNotFoundException(string message) : base(message) { }
    public EventStoreAggregateNotFoundException(string message, Exception inner) : base(message, inner) { }
}


[Serializable]
public class EventStoreCommunicationException : EventStoreException
{
    public EventStoreCommunicationException() { }
    public EventStoreCommunicationException(string message) : base(message) { }
    public EventStoreCommunicationException(string message, Exception inner) : base(message, inner) { }
}