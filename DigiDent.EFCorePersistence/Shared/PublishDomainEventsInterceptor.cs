using DigiDent.Domain.SharedKernel.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DigiDent.EFCorePersistence.Shared;

public class PublishDomainEventsInterceptor: SaveChangesInterceptor
{
    
    private readonly IPublisher _publisher;
    
    public PublishDomainEventsInterceptor(IPublisher publisher)
    {
        _publisher = publisher;
    }
    
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        PublishDomainEvents(eventData.Context).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);
    }
    
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken=default)
    {
        await PublishDomainEvents(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private async Task PublishDomainEvents(DbContext? context)
    {
        if (context is null) return;

        var entitiesWithDomainEvents = context
            .ChangeTracker
            .Entries<IHasDomainEvents>()
            .Where(entry => entry.Entity.DomainEvents.Count != 0)
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.DomainEvents;

                entity.ClearDomainEvents();

                return domainEvents;
            })
            .ToList();

        IEnumerable<Task> eventsPublishingTasks = entitiesWithDomainEvents
            .Select(async domainEvent => await _publisher.Publish(domainEvent));
        
        await Task.WhenAll(eventsPublishingTasks);
    }
}