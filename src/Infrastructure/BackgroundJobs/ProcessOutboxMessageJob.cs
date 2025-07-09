using Domain.SeedWork;
using Infrastructure.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Infrastructure.BackgroundJobs;

public class ProcessOutboxMessageJob : BackgroundService
{
    private const int ChunkValue = 20;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IPublisher _publisher;

    public ProcessOutboxMessageJob(IPublisher publisher, IServiceScopeFactory serviceScopeFactory)
    {
        _publisher = publisher;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            AppDbContext dbContext = scope.ServiceProvider.GetService<AppDbContext>();

            var unProcessedOutboxMsg = await dbContext.OutboxMessages.Where(m => m.ProcessedOnUtc == null)
                                                                     .Take(ChunkValue)
                                                                     .ToListAsync(stoppingToken);

            foreach (var outboxMsg in unProcessedOutboxMsg)
            {
                var eventType = Type.GetType(outboxMsg.Type);
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    NullValueHandling = NullValueHandling.Ignore
                };
                var domainEvent = JsonConvert.DeserializeObject<DomainEventBase>(outboxMsg.Content, settings);

                if (domainEvent == null) continue;

                //await _publisher.Publish(domainEvent, stoppingToken).ConfigureAwait(false);
                outboxMsg.ProcessedOnUtc = DateTime.UtcNow;
            }

            await dbContext.SaveChangesAsync(stoppingToken);
        }
    }
}
