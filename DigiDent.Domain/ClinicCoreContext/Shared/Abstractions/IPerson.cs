using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.SharedKernel.ValueObjects;

namespace DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;

public interface IPerson<TId, TIdValue> :
    IAggregateRoot,
    IEntity<TId, TIdValue>
    where TId : IPersonId<TIdValue>
    where TIdValue : notnull
{
    Email Email { get; }
    PhoneNumber PhoneNumber { get; }
    
    FullName FullName { get; }
    Gender Gender { get; }
    DateOnly? DateOfBirth { get; }
}