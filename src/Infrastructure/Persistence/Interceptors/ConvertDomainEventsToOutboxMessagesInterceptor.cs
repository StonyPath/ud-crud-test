using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Domain.SeedWork;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using Infrastructure.Persistence.Models;

namespace Infrastructure.Persistence.Interceptors;

public sealed class ConvertDomainEventsToOutboxMessagesInterceptor : SaveChangesInterceptor
{
    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData,
                                                           int result,
                                                           CancellationToken cancellationToken = default)
    {
        DbContext? dbContext = eventData.Context;

        if (dbContext == null) return await base.SavedChangesAsync(eventData, result, cancellationToken);

        IList<OutboxMessage>? outboxMessages = CollectOutboxMessage(dbContext.ChangeTracker);

        if (outboxMessages.Count() == 0) return await base.SavedChangesAsync(eventData, result, cancellationToken);

        await dbContext.Set<OutboxMessage>().AddRangeAsync(outboxMessages);
        await dbContext.SaveChangesAsync(cancellationToken);
        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    private List<OutboxMessage> CollectOutboxMessage(ChangeTracker changeTracker)
    {
        var outboxMessages = changeTracker.Entries()
                                          .Where(e => e.Entity is IEntity)
                                          .Select(e => (IEntity)e.Entity)
                                          .Where(e => e.DomainEvents.Count != 0)
                                          .SelectMany(e =>
                                        {
                                            var domainEvents = e.DomainEvents.ToList(); // Create a copy
                                            e.ClearDomainEvents(); // Now clearing won't affect our copy
                                            return domainEvents;
                                        })
                                          .Select(de => new OutboxMessage
                                        {
                                            Id = Guid.NewGuid(),
                                            OccurredOnUtc = DateTime.UtcNow,
                                            Type = de.GetType().Name,
                                            Content = JsonConvert.SerializeObject(de, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All })
                                        })
                                          .ToList();

        Console.WriteLine($"CollectOutboxMessage-> the outboxMessages count: {outboxMessages.Count}");
        return outboxMessages;
    }
}
