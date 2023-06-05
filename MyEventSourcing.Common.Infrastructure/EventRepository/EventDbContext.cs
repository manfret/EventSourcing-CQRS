using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyEventSourcing.Common.Domain.Core;
using MyEventSourcing.Common.Infrastructure.Data.EF;

namespace MyEventSourcing.Common.Infrastructure.EventRepository;

public class EventDbContext : BaseDbContext
{
    public DbSet<Event> Events => Set<Event>();

    public EventDbContext(DbContextOptions options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Event>(ConfigureEvents);
    }

    private void ConfigureEvents(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("Events");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.AggregateId).IsRequired();
        builder.Property(a => a.AggregateVersion).IsRequired();
        builder.Property(a => a.DomainEvent).IsRequired().EventConversion();
    }
}

public static class EventConversionExtension
{
    public static void EventConversion(this PropertyBuilder<DomainEvent> propertyBuilder)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var converter = new ValueConverter<DomainEvent, string>
        (
            v => JsonSerializer.Serialize(v, options),
            v => JsonSerializer.Deserialize<DomainEvent>(v, options)!
        );

        var comparer = new ValueComparer<DomainEvent>
        (
            (l, r) => JsonSerializer.Serialize(l, options) == JsonSerializer.Serialize(r, options),
            v => JsonSerializer.Serialize(v, options).GetHashCode(),
            v => JsonSerializer.Deserialize<DomainEvent>(JsonSerializer.Serialize(v, options), options)!
        );

        propertyBuilder.HasConversion(converter);
        propertyBuilder.Metadata.SetValueConverter(converter);
        propertyBuilder.Metadata.SetValueComparer(comparer);
        propertyBuilder.HasColumnType("jsonb");
    }
}