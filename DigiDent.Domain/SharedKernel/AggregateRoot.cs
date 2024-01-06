namespace DigiDent.Domain.SharedKernel;

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