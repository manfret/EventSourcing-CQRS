﻿namespace MyEventSourcing.Common.Domain.PubSub;

public interface ITransientDomainEventSubscriber
{
    void Subscribe<T>(Action<T> handler);
    void Subscribe<T>(Func<T, Task> handler);
}