using DigiDent.ClinicManagement.Domain.Shared.ValueObjects;
using DigiDent.Shared.Kernel.Abstractions;
using DigiDent.Shared.Kernel.ValueObjects;

namespace DigiDent.ClinicManagement.Domain.Shared.Abstractions;

public interface IPerson<TId> :
    IAggregateRoot,
    IEntity<TId, Guid>
    where TId : IPersonId
{
    Email Email { get; }
    PhoneNumber PhoneNumber { get; }
    
    FullName FullName { get; }
    Gender Gender { get; }
    DateOnly? DateOfBirth { get; }
}