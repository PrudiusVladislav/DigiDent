namespace DigiDent.Domain.SharedKernel;

public record DomainEvent(Guid Id, DateTime TimeOfOccurence);
