using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.SharedKernel.ValueObjects;

namespace DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;

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