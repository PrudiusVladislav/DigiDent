using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Domain.SharedKernel.ValueObjects;

namespace DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;

public interface IPerson
{
    Email Email { get; }
    PhoneNumber PhoneNumber { get; }
    
    FullName FullName { get; }
    Gender Gender { get; }
    DateOnly? DateOfBirth { get; }
}