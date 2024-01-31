using DigiDent.Shared.Domain.Abstractions;
using FluentAssertions;

namespace DigiDent.Shared.UnitTests.Domain.Extensions;

public static class AggregateRootValidationExtensions
{
    public static void ShouldRaiseDomainEvent<TDomainEvent>(
        this IAggregateRoot aggregateRoot)
        where TDomainEvent : IDomainEvent
    {
        aggregateRoot.DomainEvents.Should().NotBeEmpty();
        aggregateRoot.DomainEvents.Should().Contain(x => 
            x.GetType() == typeof(TDomainEvent));
    }
    
    public static void ShouldNotRaiseDomainEvent<TDomainEvent>(
        this IAggregateRoot aggregateRoot)
        where TDomainEvent : IDomainEvent
    {
        aggregateRoot.DomainEvents.Should().NotContain(x => 
            x.GetType() == typeof(TDomainEvent));
    }
}