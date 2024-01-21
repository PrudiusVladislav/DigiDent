using DigiDent.Domain.SharedKernel.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DigiDent.EFCorePersistence.Shared;

public class PublishDomainEventsInterceptor: SaveChangesInterceptor
{
    // private readonly IPublisher _publisher;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    
    public PublishDomainEventsInterceptor(
        IServiceScopeFactory serviceScopeFactory)
    {
        // _publisher = publisher;
        _serviceScopeFactory = serviceScopeFactory;
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
        
        using var scope = _serviceScopeFactory.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        
        var entries = context
            .ChangeTracker
            .Entries<IHasDomainEvents>()
            .Where(entry => entry.Entity.DomainEvents.Count != 0);
        
        var entitiesWithDomainEvents = entries
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            { 
                var domainEvents = entity.DomainEvents.ToList();

                entity.ClearDomainEvents();

                return domainEvents;
            })
            .ToList();
        
        IEnumerable<Task> eventsPublishingTasks = entitiesWithDomainEvents
            .Select(domainEvent =>  serviceProvider
                .GetRequiredService<IPublisher>()
                .Publish(domainEvent));
        
        await Task.WhenAll(eventsPublishingTasks);
    }
}