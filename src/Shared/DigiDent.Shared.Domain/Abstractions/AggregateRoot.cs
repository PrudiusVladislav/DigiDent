namespace DigiDent.Shared.Domain.Abstractions;

/// <summary>
/// Base class for all aggregate roots. Implements <see cref="IHasDomainEvents"/>.
/// </summary>
public abstract class AggregateRoot
    : IAggregateRoot
{
    private readonly List<IDomainEvent> _domainEvents = [];
    
    public IReadOnlyCollection<IDomainEvent> DomainEvents => 
        _domainEvents.AsReadOnly();
    
    protected void Raise(IDomainEvent eventItem)
    {
        _domainEvents.Add(eventItem);
    }
    
    protected void RemoveDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents.Remove(eventItem);
    }
    
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}