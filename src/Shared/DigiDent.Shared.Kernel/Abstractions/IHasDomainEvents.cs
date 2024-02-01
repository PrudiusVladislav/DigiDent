namespace DigiDent.Shared.Kernel.Abstractions;

/// <summary>
/// Ensures that entities that implement this interface have a collection of domain events.
/// Used for handling domain events in the infrastructure layer.
/// </summary>
public interface IHasDomainEvents
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}