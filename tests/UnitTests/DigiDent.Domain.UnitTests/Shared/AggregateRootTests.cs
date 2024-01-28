using DigiDent.Domain.SharedKernel.Abstractions;
using FluentAssertions;

namespace DigiDent.Domain.UnitTests.Shared;

public static class AggregateRootTests
{
    public static void ShouldRaiseDomainEvent<TDomainEvent>(
        this IAggregateRoot aggregateRoot)
        where TDomainEvent : IDomainEvent
    {
        aggregateRoot.DomainEvents.Should().NotBeEmpty();
        aggregateRoot.DomainEvents.Should().Contain(x => 
            x.GetType() == typeof(TDomainEvent));
    }
}