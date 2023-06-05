using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyEventSourcing.Common.Domain.Core;
using MyEventSourcing.Common.Domain.Data;
using MyEventSourcing.Common.Infrastructure.Data.EF;

namespace MyEventSourcing.Common.Infrastructure.EventRepository;

public class EventStore : CrudEFRepository<Event, EventDbContext>, IEventStore
{
    public EventStore(EventDbContext context, ILoggerFactory loggerFactory) 
        : base(context, loggerFactory)
    {
    }

    public async Task<IEnumerable<DomainEvent>> ReadForAggregateAsync(Guid aggregateId)
    {
        return await Context.Set<Event>()
            .Where(a => a.AggregateId == aggregateId)
            .Select(a => a.DomainEvent).ToListAsync();
    }

    public async Task AppendAsync(DomainEvent @event)
    {
        var e = new Event(@event);
        await CreateAsync(e);
    }
}