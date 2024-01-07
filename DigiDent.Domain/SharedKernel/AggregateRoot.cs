namespace DigiDent.Domain.SharedKernel;

/// <summary>
/// Base class for all aggregate roots. Implements <see cref="IHasDomainEvents"/>.
/// </summary>
public abstract class AggregateRoot
    : IHasDomainEvents 
{
    private readonly List<DomainEvent> _domainEvents = [];
    
    public IReadOnlyCollection<DomainEvent> DomainEvents => 
        _domainEvents.AsReadOnly();
    
    protected void Raise(DomainEvent eventItem)
    {
        _domainEvents.Add(eventItem);
    }
    
    protected void RemoveDomainEvent(DomainEvent eventItem)
    {
        _domainEvents.Remove(eventItem);
    }
    
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}